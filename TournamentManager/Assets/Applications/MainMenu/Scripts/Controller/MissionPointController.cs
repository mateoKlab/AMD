using UnityEngine;
using System.Collections;
using Bingo;

public class MissionPointController : Controller <MainMenu, MissionPointModel, MissionPointView>
{
	public void ShowMissionPopUp() {
		app.view.missionView.gameObject.SetActive(true);
		app.controller.footerController.DisableButtons();
		app.controller.missionController.SetMissionData(((MissionPointModel)model));
	}
}
