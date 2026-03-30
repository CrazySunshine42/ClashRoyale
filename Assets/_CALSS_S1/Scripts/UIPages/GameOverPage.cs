using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class GameOverPage : UIPage
{
	public Button button;
	public Image win;
	public Transform kingRed;
	public Transform kingBlue;

	protected override string uiPath => "GameOverPage";

	protected override void OnAwake()
	{
		button = transform.Find("Button").GetComponent<Button>();
		win = transform.Find("Win").GetComponent<Image>();
		kingRed = transform.Find("KingRed");
		kingBlue = transform.Find("KingBlue");

		OnStart();
	}
}