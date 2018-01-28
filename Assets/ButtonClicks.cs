using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonClicks : MonoBehaviour {

    public string mainScene;
    public string gameScene;
    public string creditsScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene(creditsScene);
    }

    public void GotoMainMenu()
    {
        SceneManager.LoadScene(mainScene);
    }
}
