using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class LogoPage : UIPage
{
	public Slider slider;


	protected override string uiPath => "LogoPage";

	protected override void OnAwake()
	{
		slider = transform.Find("Slider").GetComponent<Slider>();

		OnStart();
	}
}