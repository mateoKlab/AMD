using UnityEngine;
using System.Collections;
using Bingo;

public class MainMenuController : Controller<MainMenu>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public GachaController gachaController { get; private set; }
    
    [Inject]
    public TownController townController { get; private set; }
    
    [Inject]
    public FooterController footerController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////

	public void GoToBattleScene(params object[] args) 
	{
		Application.LoadLevel("BattleScene");
	}

	public void ShowTownPopUp(params object[] args) 
	{
		footerController.DisableButtons();
		app.view.popUpShadeView.gameObject.SetActive(true);
		app.view.townView.gameObject.SetActive(true);
	}

	public void ShowStablePopUp(params object[] args)
	{
		Debug.LogError("Stable");
	}

	public void ShowGachaPopUp(params object[] args) 
	{
		footerController.DisableButtons();
		app.view.popUpShadeView.gameObject.SetActive(true);
		app.view.gachaView.gameObject.SetActive(true);
	}
}

