﻿using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameEvent
{
	public string Key = string.Empty;
	public string Text = string.Empty;
	public string ImageId = string.Empty;
	public List<string> Flags = new List<string>();
	public List<string> ClearFlags = new List<string>();
	public List<string> SoundIds = new List<string>();
	public List<float> SoundVolumes = new List<float>();
	public List<EventOption> Options = new List<EventOption>();
}

[System.Serializable]
public class EventOption
{
	public string Text = string.Empty;
	public List<EventTarget> Targets = new List<EventTarget>();
	public List<string> RequiredFlags = new List<string>();
	public List<string> NotAllowedFlags = new List<string>();

	public EventTarget Target
	{
		get
		{
			if (Targets.IsNullOrEmpty())
			{
				Debug.LogError("Event Option " + Text + " does not have any targets");
				return null;
			}
			return Targets[Random.Range(0, Targets.Count)];
		}
	}
}
[System.Serializable]
public class EventTarget
{
	public string Key;
	public bool ShouldWait;
	public EventTarget(string key, bool shouldWait)
	{
		Key = key;
		ShouldWait = shouldWait;
	}
}