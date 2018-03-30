using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonBehavior : Button {

    AudioSource audioSource;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        onClick.AddListener(delegate { play(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void play()
    {
        audioSource.Play();
    }
}
