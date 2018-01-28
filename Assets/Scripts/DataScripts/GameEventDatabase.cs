using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName ="GameEventDatabase", menuName = "GameEventDatabase")]
public class GameEventDatabase : ScriptableObject
{
	[SerializeField]
	private List<GameEvent> _database;
	public string NextEvent = "Start";


	void OnEnable()
	{
		if (_database == null)
			_database = new List<GameEvent>();
	}

	public int Add(GameEvent e)
	{
		_database.Add(e);
		return _database.Count - 1;
	}

	public void Remove(GameEvent e)
	{
		_database.Remove(e);
	}

	public void RemoveAt(int index)
	{
		_database.RemoveAt(index);
	}
	public void Clear()
	{
		_database.Clear();
	}

	public int Count
	{
		get { return _database.Count; }
	}

	public int IndexOf(string key)
	{
		return _database.IndexOf(this[key]);
	}

	int depth = 0;

	public GameEvent this[int key]
	{
		get
		{
			depth = 0;
			return _database[key];
		}
		set
		{
			_database[key] = value;
		}
	}
	public GameEvent this[string key]
	{
		get
		{
			depth++;

			if (depth > 20)
			{
				Debug.LogError("Infinite loop in GameEventDatabase string indexer");
				return null;
			}
			var events = _database.Where(l => l.Key == key);
			if (events.IsNullOrEmpty())
			{
				//Debug.LogError("There is no event with key [" + key + "]");
				return this["TheGameBroke"];
			}
			var list = events.ToList();
			depth--;
			return list[Random.Range(0, list.Count)];
		}
	}
}