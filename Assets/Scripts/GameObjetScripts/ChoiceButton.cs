using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceButton : Button
{
	public string ChoiceText;

	Slider slider;
	TextMeshProUGUI textElement;
	LayoutElement layoutElement;
	protected override void Start()
	{
		base.Start();
		textElement = GetComponentInChildren<TextMeshProUGUI>();
		layoutElement = GetComponent<LayoutElement>();
		slider = GameObject.Find("ButtonTextSlider").GetComponent<Slider>();
	}
	void Update()
	{
		textElement.text = ChoiceText;
		textElement.ForceMeshUpdate();
		layoutElement.preferredHeight = textElement.preferredHeight + 8;
		textElement.fontSize = (int)slider.value;
	}
}
