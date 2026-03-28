using System.Collections.Generic;
using UnityEngine;

public partial class MainPage
{
	public MainPage() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
	{
		Debug.LogWarning("TODO: 请修改MainPage页面类型等参数，或注释此行");
	}

	public void OnStart()
	{
		//KBEngine.Event.registerOut("MyEventName", this, "MyEventHandler");
		midBottom.onClick.AddListener(OnMidBottom);
	}
	public void OnMidBottom()
	{
		Debug.Log("点击进入战斗界面");
	}
	//public void MyEventHandler()
	//{
	//}
}
