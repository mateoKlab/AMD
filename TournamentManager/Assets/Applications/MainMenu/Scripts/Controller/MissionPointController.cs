using UnityEngine;
using System.Collections;
using Bingo;

public class MissionPointController : Controller
{
	public void ShowMissionPopUp() {
		app.GetComponent<MainMenuView>().missionView.gameObject.SetActive(true);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(true);
		app.GetComponent<MainMenuController>().footerController.DisableButtons();
		app.GetComponent<MainMenuController>().missionController.SetMissionData(((MissionPointModel)model));
	}
}
