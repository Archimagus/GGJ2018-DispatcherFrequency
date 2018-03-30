using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(TextMeshProUGUI))]
public class CreditsReader : MonoBehaviour
{
	Text textBox;
	[SerializeField]
	TextAsset creditsAsset;

	public bool crawling;
	public float speed;
	public float endPosition;
	public TextMeshProUGUI thanks;
	public float ticksForAlphaChange;
	public float colorTicks = 0;
	public GameObject mainSceneButton;

	void Awake()
	{
		thanks.color = Color.clear;
		//Color zm = thanks.color;  //  makes a new color zm
		//zm.a = 0.0f; // makes the color zm transparent
	}

	void OnEnable()
	{
		GetComponent<Text>().text = creditsAsset.text;    
	}

	void Update()
	{

		//if (thanks.color.a >= 255f)
		//{
			
		//    return;
		//}

		if (gameObject.transform.position.y > endPosition)
		{
			crawling = false;
		}


		if (crawling)
		{
			transform.Translate(Vector3.up * Time.deltaTime * speed);
		} 
		else
		{
			thanks.color = Color.white;
			mainSceneButton.SetActive(true);
		}
		//} else if (colorTicks <= ticksForAlphaChange)
		//{
		//    colorTicks += Time.deltaTime;
		//    print(Mathf.Ceil(255f * (colorTicks / ticksForAlphaChange)));
		//    thanks.color = new Color(255f, 255f, 255f, (float) MathfMathf.Ceil(255f * (colorTicks / ticksForAlphaChange))); // makes the color from transparent to solid
		//}
		
		
	}
}


