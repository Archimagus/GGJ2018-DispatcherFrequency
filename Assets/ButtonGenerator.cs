using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonGenerator : MonoBehaviour {

    public PawnBehavior pawn;
    public GameObject _button;
    //List<GameObject> options;
	// Use this for initialization
	void Start () {
        
        GameObject button = Instantiate(_button);
        button.transform.GetChild(0).GetComponent<Text>().text = pawn.pawnOption;
        Button buttonComp = button.GetComponent<Button>();
        //buttonComp.onClick.AddListener(delegate { pawn.deployPawn(); });
        button.transform.SetParent(transform);
        button.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //options.Add(Instantiate(button));



        //int buttonLength = pawn.pawnOptions.Length;

        //for(int i = 0; i < buttonLength; i++)
        //{
        //    GameObject button = Instantiate(_button);
        //    Button buttonComp = button.GetComponent<Button>();
        //    buttonComp.onClick.AddListener(delegate { pawn.deployPawn(); });
        //    options.Add(Instantiate(button));
            
        //}

        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
