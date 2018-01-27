using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
	[SerializeField]
	RadioClip[] _clips;
	[SerializeField]
	AudioClip _static;
	[SerializeField]
	AudioMixerGroup _musicMixerGroup;

	[SerializeField]
	float _minVolume;
	[SerializeField]
	float _maxVolume;
	public float Tuning { get; set; }
	public float Volume
	{
		get
		{
			return Mathf.Lerp(_minVolume, _maxVolume, _musicVolume);
		}
		set
		{
			float newVal = Mathf.InverseLerp(_minVolume, _maxVolume, value);

			if (Mathf.Abs(_musicVolume - newVal) > 0.01f)
			{
				_musicMixerGroup.audioMixer.SetFloat("MusicVolume", LinearToDecibel(newVal));
				Debug.Log("Setting music to " + newVal);
			}

			_musicVolume = newVal;
		}
	}

	int _currentClip = 0;

	AudioSource _musicSource;
	AudioSource _staticSource;
	private float _musicVolume;


	// Use this for initialization
	void Start ()
	{
		var ms = new GameObject("MusicSource");
		_musicSource = ms.AddComponent<AudioSource>();
		_musicSource.outputAudioMixerGroup = _musicMixerGroup;

		var ss = new GameObject("StaticSource");
		_staticSource = ss.AddComponent<AudioSource>();
		_musicSource.outputAudioMixerGroup = _musicMixerGroup;
		_staticSource.clip = _static;
		_staticSource.volume = 0.1f;
		_staticSource.loop = true;
		_staticSource.Play();

		_musicSource.clip = _clips[0].Clip;
		_musicSource.loop = true;
		_musicSource.Play();
	}
	
	// Update is called once per frame
	void Update ()
	{
		int closest = -1;
		float closestValue = float.MaxValue;
		for (int i = 0; i < _clips.Length; i++)
		{
			float val = Mathf.Abs(_clips[i].TuneValue - Tuning);
			if (val < closestValue)
			{
				closestValue = val;
				closest = i;
			}
		}
		if(_currentClip != closest)
		{
			_musicSource.clip = _clips[closest].Clip;
			_currentClip = closest;
			_musicSource.Play();
		}

		_musicSource.volume = Mathf.Clamp01(1-(closestValue/20));
		_staticSource.volume = Mathf.Clamp((closestValue / 20), 0.1f, 1.0f);

	}



	private static float LinearToDecibel(float lin)
	{
		if (lin <= float.Epsilon)
			return -80;
		return Mathf.Log(lin, 3) * 20;
	}

	private static float DecibelToLinear(float db)
	{
		return Mathf.Pow(3, db / 20);
	}
}

[System.Serializable]
public class RadioClip
{
	public AudioClip Clip;
	public float TuneValue;
}