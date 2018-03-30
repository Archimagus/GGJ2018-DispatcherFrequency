using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate { quit(); });
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void quit()
    {
        Application.Quit();
    }
}
