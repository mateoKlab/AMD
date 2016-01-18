using UnityEngine;
using System.Collections;
using Bingo;

public class MissionPointController : Controller <MainMenu, MissionPointModel, MissionPointView>
{
	public void ShowMissionPopUp() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button2");
		app.view.missionView.gameObject.SetActive(true);
		app.controller.footerController.DisableButtons();
		app.controller.missionController.SetMissionData(((MissionPointModel)model));
	}
}
