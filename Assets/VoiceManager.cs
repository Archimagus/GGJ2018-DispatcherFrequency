using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VoiceManager : MonoBehaviour
{
	[SerializeField]
	AudioMixerGroup _voiceMixerGroup;

	private float _voiceVolume;

	[SerializeField]
	float _minVolume;
	[SerializeField]
	float _maxVolume;

	public float Volume
	{
		get
		{
			return Mathf.Lerp(_minVolume, _maxVolume, _voiceVolume);
		}
		set
		{
			float newVal = Mathf.InverseLerp(_minVolume, _maxVolume, value);

			if (Mathf.Abs(_voiceVolume - newVal) > 0.01f)
			{
				_voiceMixerGroup.audioMixer.SetFloat("VoiceVolume", LinearToDecibel(newVal));
			}

			_voiceVolume = newVal;
		}
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