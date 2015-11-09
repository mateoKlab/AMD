using UnityEngine;
using System.Collections;
using Bingo;

public class TownController : Controller
{	
	public void CloseTownPopUp(params object[] args) 
	{
		gameObject.SetActive(false);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(false);
		app.GetComponent<MainMenuController>().footerController.EnableButtons();
	}
}
