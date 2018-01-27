using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName ="GameEventDatabase", menuName = "GameEventDatabase")]
public class GameEventDatabase : ScriptableObject
{
	[SerializeField]
	private List<GameEvent> database;
	public string textPath;

	void OnEnable()
	{
		if (database == null)
			database = new List<GameEvent>();
		//if (!database.Any(g => g.Key == "TheGameBroke"))
		//{
		//	database.Add(new GameEvent()
		//	{
		//		Key = "TheGameBroke",
		//		Text = "You Died because the game developers didn’t write a scenario to handle that last choice.",
		//		Options = new List<EventOption> { new EventOption { Text = "Well damn, I guess I’ll start over!.", Targets = new List<string> { "[Start]" } } }
		//	});
		//}
	}

	public int Add(GameEvent e)
	{
		database.Add(e);
		return database.Count - 1;
	}

	public void Remove(GameEvent e)
	{
		database.Remove(e);
	}

	public void RemoveAt(int index)
	{
		database.RemoveAt(index);
	}
	public void Clear()
	{
		database.Clear();
	}

	public int Count
	{
		get { return database.Count; }
	}

	public int IndexOf(string key)
	{
		return database.IndexOf(this[key]);
	}

	int depth = 0;
	public GameEvent this[int key]
	{
		get
		{
			depth = 0;
			return database[key];
		}
		set
		{
			database[key] = value;
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
			var events = database.Where(l => l.Key == key);
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