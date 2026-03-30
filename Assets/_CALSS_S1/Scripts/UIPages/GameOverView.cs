using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using DG.Tweening;
using static UnityRoyale.Placeable;

public partial class GameOverPage
{
	public GameOverPage() : base(UIType.Normal, UIMode.DoNothing, UICollider.None)
	{
		Debug.LogWarning("TODO: 请修改GameOverPage页面类型等参数，或注释此行");
	}

	public void OnStart()
	{
		//KBEngine.Event.registerOut("MyEventName", this, "MyEventHandler");
		this.button.onClick.AddListener(GameOverButton);
	}
	public void GameOverButton()
	{
		CloseAllPages();
		Addressables.LoadSceneAsync("Main");
		ShowPageAsync<MainPage>();
	}
    protected override void OnActive()
    {
		var faction = (Faction)data;
		var winner = faction == Faction.Player?kingRed : kingBlue;
		CanvasGroup cg = winner.GetComponent<CanvasGroup>();
		cg.DOFade(1, 1.5f);
		winner.DOShakeScale(1.5f);
		win.transform.localPosition = winner.localPosition;
	}
	//public void MyEventHandler()
	//{
	//}
}
