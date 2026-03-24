using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public partial class LogoPage
{
	private float showSecond = 10f;
	public LogoPage() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
	{
		Debug.LogWarning("TODO: 请修改LogoPage页面类型等参数，或注释此行");
	}

	public void OnStart()
	{
		//KBEngine.Event.registerOut("MyEventName", this, "MyEventHandler");
		slider.DOValue(1, showSecond);
	}

	//public void MyEventHandler()
	//{
	//}
}
