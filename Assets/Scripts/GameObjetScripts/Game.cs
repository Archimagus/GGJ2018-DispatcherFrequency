using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
	[SerializeField]
	ChoiceButton _choiceButtonPrefab;
	//[SerializeField]
	//GameEventDatabase[] _eventDatabases;
    [SerializeField]
    GameEventDatabase _eventDatabase;
    [SerializeField]
	ScrollRect _textAreaScrollView;
	[SerializeField]
	Transform _choicesArea;

	Text _eventText;
	StringBuilder _story = new StringBuilder();
	HashSet<string> _flags = new HashSet<string>();
	
	AudioSource _audioElement;
    GameEventDatabase currentDB;
    //Random random = new Random();

    void Start()
	{
		_eventText = _textAreaScrollView.GetComponentInChildren<Text>();
		//if (_eventDatabases.Length < 1)
		//{
		//	Debug.Log("GameEventDatabase not set, looking for one in resources.");
  //          _eventDatabases = Resources.FindObjectsOfTypeAll<GameEventDatabase>();
		//}
		//if (_eventDatabases.Length < 1)
		//	Debug.LogError("Unable to find a GameEventDatabase.");

        if (_eventDatabase == null)
        {
            Debug.Log("GameEventDatabase not set, looking for one in resources.");
            _eventDatabase = Resources.FindObjectsOfTypeAll<GameEventDatabase>().First();
        }
        if (_eventDatabase == null)
            Debug.LogError("Unable to find a GameEventDatabase.");

        GoToEvent("Start");
	}
    void Update()
    {
        //int randomNumber = Random.Range(0, _eventDatabases.Length - 1);
        //currentDB = _eventDatabases[randomNumber];
    }
    IEnumerator UpdateScroll()
	{
		yield return null;
		_textAreaScrollView.verticalNormalizedPosition = 0;
	}
	void GoToEvent(string key)
	{
		if (key == "Start")
		{
			_story = new StringBuilder();
			_audioElement = GetComponent<AudioSource>();
		}

        var e = _eventDatabase[key];
        //var e = currentDB[key];        

		_eventText.text = "<color=grey>" + _story.ToString() + "</color>" + "<color=white>" + e.Text + "</color>";
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
		for(int i = 0; i < _choicesArea.childCount; i++)
		{
			Destroy(_choicesArea.GetChild(i).gameObject);
		}

		foreach (var opt in e.Options)
		{
			if (opt.RequiredFlags.Any(f => !_flags.Contains(f))) // If we are missing any of the required flags dont add this option
				continue;
			if (opt.NotAllowedFlags.Any(f => _flags.Contains(f))) // If we have any of the not allowed flags don't add this option
				continue;

			var o = opt;
			var b = Instantiate(_choiceButtonPrefab);
			b.ChoiceText = o.Text;
			b.onClick.AddListener(() => GoToEvent(o.Target));
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
}
