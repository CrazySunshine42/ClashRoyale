using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class MainPage : UIPage
{
	public Button playerInfo;
	public Button forceIcon;
	public Text cupNumber;
	public Text playerName;
	public Text forceName;
	public Button settingIcon;
	public Button freeChestButton;
	public Text timeText;
	public Button royalChestButton;
	public Slider chestNumSlider;
	public Text numberText;
	public Button cupIcon;
	public Button taskIcon;
	public Button tVIcon;
	public Button starIcon;
	public Image midTop;
	public Button midBottom;


	protected override string uiPath => "MainPage";

	protected override void OnAwake()
	{
		playerInfo = transform.Find("Top1/PlayerInfo").GetComponent<Button>();
		forceIcon = transform.Find("Top1/ForceIcon").GetComponent<Button>();
		cupNumber = transform.Find("Top1/CupNumber").GetComponent<Text>();
		playerName = transform.Find("Top1/PlayerName").GetComponent<Text>();
		forceName = transform.Find("Top1/ForceName").GetComponent<Text>();
		settingIcon = transform.Find("Top1/SettingIcon").GetComponent<Button>();
		freeChestButton = transform.Find("Top2/FreeChestButton").GetComponent<Button>();
		timeText = transform.Find("Top2/FreeChestButton/TimeText").GetComponent<Text>();
		royalChestButton = transform.Find("Top2/RoyalChestButton").GetComponent<Button>();
		chestNumSlider = transform.Find("Top2/RoyalChestButton/CrownImage/ChestNumSlider").GetComponent<Slider>();
		numberText = transform.Find("Top2/RoyalChestButton/CrownImage/NumberText").GetComponent<Text>();
		cupIcon = transform.Find("MidLeft/CupIcon").GetComponent<Button>();
		taskIcon = transform.Find("MidLeft/TaskIcon").GetComponent<Button>();
		tVIcon = transform.Find("MidRight/TVIcon").GetComponent<Button>();
		starIcon = transform.Find("MidRight/StarIcon").GetComponent<Button>();
		midTop = transform.Find("MidTop").GetComponent<Image>();
		midBottom = transform.Find("MidBottom").GetComponent<Button>();

		OnStart();
	}
}