using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class BottomFixPage : UIPage
{
	public Button cupIcon;
	public Button cardIcon;
	public Button battleIcon;
	public Button chatIcon;
	public Button tVIcon;


	protected override string uiPath => "BottomFixPage";

	protected override void OnAwake()
	{
		cupIcon = transform.Find("Scroll View/Viewport/Content/CupIcon").GetComponent<Button>();
		cardIcon = transform.Find("Scroll View/Viewport/Content/CardIcon").GetComponent<Button>();
		battleIcon = transform.Find("Scroll View/Viewport/Content/BattleIcon").GetComponent<Button>();
		chatIcon = transform.Find("Scroll View/Viewport/Content/ChatIcon").GetComponent<Button>();
		tVIcon = transform.Find("Scroll View/Viewport/Content/TVIcon").GetComponent<Button>();

		OnStart();
	}
}