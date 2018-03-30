using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScr : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(chill());
    }
	
    IEnumerator chill()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("MainScene");
    }

	// Update is called once per frame
	void Update () {
       
	}
}
