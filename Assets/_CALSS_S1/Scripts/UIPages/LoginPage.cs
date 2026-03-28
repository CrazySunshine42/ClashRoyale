using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class LoginPage : UIPage
{
	public InputField accInput;
	public InputField pwInput;
	public Button button;


	protected override string uiPath => "LoginPage";

	protected override void OnAwake()
	{
		accInput = transform.Find("Acc/AccInput").GetComponent<InputField>();
		pwInput = transform.Find("Pw/PwInput").GetComponent<InputField>();
		button = transform.Find("Button").GetComponent<Button>();

		OnStart();
	}
}