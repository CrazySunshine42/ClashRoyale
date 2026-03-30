using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public partial class LogoPage
{
	private float showSecond = 4f;
	public LogoPage() : base(UIType.Normal, UIMode.HideOther, UICollider.None)
	{
		Debug.LogWarning("TODO: 请修改LogoPage页面类型等参数，或注释此行");
	}

	public void OnStart()
	{
		//KBEngine.Event.registerOut("MyEventName", this, "MyEventHandler");
		slider.DOValue(1, showSecond).OnComplete(() =>
		{
			Addressables.LoadSceneAsync("Main");
		});
	}

	//public void MyEventHandler()
	//{
	//}
}
