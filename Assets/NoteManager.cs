using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NoteManager : MonoBehaviour {

    public GameObject mainCamera;
    public GameObject notePrefab;
    Dictionary<string, GameObject> notes = new Dictionary<string, GameObject>();
    Vector3 nextPosition;
    float movesX = 0f;
    float movesY = 0f;


    public string selectedNoteName;
    GameObject selectednote;

	// Use this for initialization
	void Start () {
        print("Camera Position: " + mainCamera.transform.position);
        nextPosition = new Vector3(mainCamera.transform.position.x, mainCamera.transform.position.y, -0.1f);
    }
	
	// Update is called once per frame
	void Update () {
        print("Next Position Value: " + nextPosition);
        adjustNextPosition();
	}


    public void Create(string text)
    {
        GameObject note = Instantiate(notePrefab);
        note.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        //note.name = note.GetHashCode().ToString();
        note.name = note.GetHashCode().ToString();
        //print("Old Note position: " + note.transform.position);
        //print("Old Note X: " + note.transform.position.x);
        //print("Old Note Y: " + note.transform.position.y);
        //print("Old Note Z: " + note.transform.position.z);
        note.GetComponent<AdjustPosition>().adjust(new Vector3(nextPosition.x, nextPosition.y, -0.1f));
        note.transform.position = new Vector3(nextPosition.x, nextPosition.y, -0.1f);
        //print("New Note position: " + note.transform.position);
        //print("New Note X: " + note.transform.position.x);
        //print("New Note Y: " + note.transform.position.y);
        //print("New Note Z: " + note.transform.position.z);
        nextPosition += new Vector3(nextPosition.x + movesX, nextPosition.y, nextPosition.z); ;
        notes.Add(note.name, note);
        movesX += 1f;
    }

    //public void Create(string text, string key)
    //{
    //    GameObject note = Instantiate(notePrefab);
    //    note.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
    //    //note.name = note.GetHashCode().ToString();
    //    note.name = key;
    //    note.transform.position = nextPosition;
    //    nextPosition += Vector3.right * 2;
    //    notes.Add(note.name, note);
    //}

    //public void selectNote()
    //{
    //    Dictionary<string, GameObject>.KeyCollection
    //        keys = notes.Keys;

    //    string[] _keys = new string[keys.Count];
    //    keys.CopyTo(_keys, 0);

    //    int randomNum = Random.Range(0, keys.Count);

    //    print("Selected A Note: " + notes.TryGetValue(_keys[randomNum], out selectednote));
    //    print("Note: "  + selectednote.name);
    //}

    public void adjustNextPosition()
    {
        if(movesX % 5 == 0 && movesY > 0)
        {
            movesX = 0;
            movesY += 1;
            nextPosition = new Vector3(nextPosition.x, nextPosition.y + movesY, nextPosition.z);
        }
    }

    public void Remove(string key)
    {

        //if (notes.ContainsKey(selectednote.name))
        //{
        //    //notes.Remove(selectednote.name);
        //    //Destroy(selectednote);
        //}

        if (notes.ContainsKey(key))
        {
            GameObject note;
            notes.TryGetValue(key, out note);
            notes.Remove(key);
            Destroy(note);
        }


        //if (notes.ContainsKey(noteHash))
        //{
        //    notes.Remove(noteHash);
        //}
    }

}
