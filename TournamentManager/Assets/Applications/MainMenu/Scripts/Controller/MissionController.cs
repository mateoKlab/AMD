using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionController : Controller
{
	private MissionPointModel currentMissionPoint;
   
	public void GoToMission(params object[] args) {
		Debug.Log ("Mission: " + args[0]);
	}
	
	public void CloseMissionPopUp(params object[] args) 
	{
		gameObject.SetActive(false);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(false);
		app.GetComponent<MainMenuController>().footerController.EnableButtons();
	}

	public void SetMissionData(MissionPointModel mPointModel) {
		currentMissionPoint = mPointModel;
		((MissionView)view).missionImage.texture = Resources.Load(mPointModel.imagePath + mPointModel.missionPointData.name.Split(' ')[0]) as Texture;
		((MissionView)view).missionName.text = mPointModel.missionPointData.name.ToUpper();
	}

	
	public void StartMissionMatch() {
		GameData.instance.currentStage =  currentMissionPoint.missionPointData;
		Messenger.Send(MainMenuEvents.START_BATTLE, currentMissionPoint.missionPointData.id);
	}
}
