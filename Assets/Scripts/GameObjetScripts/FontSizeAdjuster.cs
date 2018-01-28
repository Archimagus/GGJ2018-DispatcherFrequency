using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FontSizeAdjuster : MonoBehaviour
{
	public float FontSize
	{
		get
		{
			if (text != null)
				return text.fontSize;
			if (tmPro != null)
				return tmPro.fontSize;
			return 0;
		}
		set
		{
			if (text != null)
				text.fontSize = (int)value;
			if (tmPro != null)
				tmPro.fontSize = (int)value;

		}
	}
	Text text;
	TextMeshProUGUI tmPro;
	//Slider slider;
	void Start()
	{
		text = GetComponent<Text>();
		tmPro = GetComponent<TextMeshProUGUI>();
		//slider = GameObject.Find("TextSizeSlider").GetComponent<Slider>();
	}
	//void Update()
	//{
	//	text.fontSize = (int)slider.value;
	//}
}
