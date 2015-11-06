using UnityEngine;
using System.Collections;
using Bingo;

public class GachaController : Controller
{
	public void CloseGachaPopUp(params object[] args) 
	{
		gameObject.SetActive(false);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(false);
		app.GetComponent<MainMenuController>().footerController.EnableButtons();
	}

	public void GenerateRandomCharacter() {

	}
}
