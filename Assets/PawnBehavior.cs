using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PawnBehavior : MonoBehaviour {

    public string role;
    public float successRate;
    public string pawnOption;
    public Text eventOutcome;
    public string currentEvent;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //public void deployPawn()
    //{
    //    print("Deployed: " + role + " with a success rate of " + successRate);

    //    if(successRate < 70)
    //    {
    //        eventOutcome.text = "You chose incorrectly and failed mission:\n" + currentEvent; 
    //    }
    //    else
    //    {
    //        eventOutcome.text = "You chose correctly and passed mission:\n" + currentEvent;
    //    }
    //}
}
