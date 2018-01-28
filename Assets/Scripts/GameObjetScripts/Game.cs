using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	[SerializeField]
	ChoiceButton _choiceButtonPrefab;
	[SerializeField]
	GameEventDatabase []_eventDatabases;
	[SerializeField]
	ScrollRect _textAreaScrollView;
	[SerializeField]
	Transform _choicesArea;

	TextMeshProUGUI _eventText;
	StringBuilder _story = new StringBuilder();
	HashSet<string> _flags = new HashSet<string>();
	
	AudioSource _audioElement;

	void Start()
	{
		_audioElement = GetComponent<AudioSource>();
		_eventText = _textAreaScrollView.GetComponentInChildren<TextMeshProUGUI>();
		if (_eventDatabases.IsNullOrEmpty())
		{
			Debug.Log("GameEventDatabase not set, looking for one in resources.");
			_eventDatabases = Resources.FindObjectsOfTypeAll<GameEventDatabase>()?.ToArray();
		}
		if (_eventDatabases.IsNullOrEmpty())
			Debug.LogError("Unable to find a GameEventDatabase.");
		GoToEvent(_eventDatabases[0], "Start");
	}
	IEnumerator UpdateScroll()
	{
		yield return null;
		_textAreaScrollView.verticalNormalizedPosition = 0;
	}
	void GoToEvent(GameEventDatabase database, EventTarget target)
	{
		if(target.ShouldWait)
		{
			database.NextEvent = target.Key;
			StartCoroutine(startRandomEvent());

		}
		else
		{
			GoToEvent(database, target.Key);
		}
	}
	IEnumerator startRandomEvent()
	{
		_eventText.text = "<color=#dfdfdf>" + _story.ToString() + "</color>";
		ClearButtons();

		yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
		var db = _eventDatabases[Random.Range(0, _eventDatabases.Length)];
		GoToEvent(db, db.NextEvent);

	}
	void GoToEvent(GameEventDatabase database, string key)
	{
		if (key == "Start")
		{
			_story = new StringBuilder();
		}
		var e = database[key];
		_eventText.text = "<color=#dfdfdf>" + _story.ToString() + "</color>" + e.Text;
		_story.Append(e.Text);
		StartCoroutine(UpdateScroll());


		e.Flags.ForEach(f => _flags.Add(f));
		e.ClearFlags.ForEach(f => _flags.RemoveWhere(f2 => f2 == f));

		var opts = new List<EventOption>();
		foreach (var opt in e.Options)
		{
			if (opt.RequiredFlags.Any(f => !_flags.Contains(f))) // If we are missing any of the required flags dont add this option
				continue;
			if (opt.NotAllowedFlags.Any(f => _flags.Contains(f))) // If we have any of the not allowed flags don't add this option
				continue;
			opts.Add(opt);
		}
		ClearButtons();

		foreach (var opt in e.Options)
		{
			if (opt.RequiredFlags.Any(f => !_flags.Contains(f))) // If we are missing any of the required flags dont add this option
				continue;
			if (opt.NotAllowedFlags.Any(f => _flags.Contains(f))) // If we have any of the not allowed flags don't add this option
				continue;

			var o = opt;
			var b = Instantiate(_choiceButtonPrefab);
			b.ChoiceText = o.Text;
			b.onClick.AddListener(() => GoToEvent(database, o.Target));
			b.transform.SetParent(_choicesArea, false);
		}
		/* TODO FIX MULTIPLE SOUNDS */

		for (int i = 0; i < e.SoundIds.Count; i++)
		{
			var soundId = e.SoundIds[i];
			var soundVolume = e.SoundVolumes[i];
			var ac = Resources.Load<AudioClip>(soundId);
			if (ac != null)
			{

				if (_audioElement != null)
				{
					//audioElement.PlayOneShot(ac, soundVolume);
					_audioElement.Stop();
					_audioElement.clip = ac;
					_audioElement.Play();
				}
			}
			else
			{
				Debug.LogError("No audio clip found for " + soundId);
			}
		}
	}

	private void ClearButtons()
	{
		for (int i = 0; i < _choicesArea.childCount; i++)
		{
			Destroy(_choicesArea.GetChild(i).gameObject);
		}
	}
}
