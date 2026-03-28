using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class TopFixPage : UIPage
{
	public Slider slider;
	public Text levelNum;
	public Text goldNum;
	public Button goldButton;
	public Text gemNum;
	public Button gemButton;


	protected override string uiPath => "TopFixPage";

	protected override void OnAwake()
	{
		slider = transform.Find("Lv/Slider").GetComponent<Slider>();
		levelNum = transform.Find("Lv/LevelImg/LevelNum").GetComponent<Text>();
		goldNum = transform.Find("Gold/BG/GoldNum").GetComponent<Text>();
		goldButton = transform.Find("Gold/GoldButton").GetComponent<Button>();
		gemNum = transform.Find("Gem/BG/GemNum").GetComponent<Text>();
		gemButton = transform.Find("Gem/GemButton").GetComponent<Button>();

		OnStart();
	}
}