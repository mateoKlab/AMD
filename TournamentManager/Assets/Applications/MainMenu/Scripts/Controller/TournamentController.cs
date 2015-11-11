using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentController : Controller
{
	public void CloseTournamentPopUp(params object[] args) 
	{
		gameObject.SetActive(false);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(false);
		app.GetComponent<MainMenuController>().footerController.EnableButtons();
	}

	public void GoToBattleScene(params object[] args) 
	{
		Application.LoadLevel("BattleScene");
	}
}
