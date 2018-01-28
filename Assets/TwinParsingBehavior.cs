using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cradle;

public class TwinParsingBehavior : MonoBehaviour {

    public Story story;
    string[] currentPassagetags;

	// Use this for initialization
	void Start () {
        story.Begin();
        currentPassagetags = story.CurrentPassage.Tags;
	}
	
	// Update is called once per frame
	void Update () {
        print(currentPassagetags[0]);
	}

    void story_OnOutput(StoryOutput output)
    {
        Debug.Log(output.Text);
    }
}
