using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void adjust(Vector3 position)
    {
        //print("Inside Adjust: " + position);
        //print("Position: " + position);
        //print("Old Position: " + transform.position);
        transform.position = position;
        //print("Current Position: " + transform.position);
    }
}
