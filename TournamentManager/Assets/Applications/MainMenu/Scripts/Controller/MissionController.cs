using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionController : Controller <MainMenu, MissionModel, MissionView>
{
	private MissionPointModel currentMissionPoint;
   
	 public void GoToMission(params object[] args) {
		Debug.Log ("Mission: " + args[0]);
	}

	public void SetMissionData(MissionPointModel mPointModel) {
		currentMissionPoint = mPointModel;
		view.missionImage.texture = Resources.Load(mPointModel.imagePath + mPointModel.missionPointData.name.Split(' ')[0]) as Texture;
		view.missionName.text = mPointModel.missionPointData.name.ToUpper();
		view.enemyCountLabel.text = mPointModel.missionPointData.enemies.Count.ToString();
		view.goldRewardLabel.text = mPointModel.missionPointData.goldReward.ToString();
		view.expRewardLabel.text = mPointModel.missionPointData.xpReward.ToString();
		GetComponent<Animator>().SetTrigger("TransitionIn");
	}

	
	public void StartMissionMatch() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		GetComponent<Animator>().SetTrigger("TransitionToBattle");
		GameData.instance.currentStage =  currentMissionPoint.missionPointData;
		//Messenger.Send(MainMenuEvents.START_BATTLE, currentMissionPoint.missionPointData.id);
		app.controller.GoToBattleScene();
	}

	public void TransitionOut() {
		StartCoroutine(TransitionOutCoroutine());
	}

	IEnumerator TransitionOutCoroutine() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}

	public void PlaySFXEvent(string sfxName) {
		SoundManager.instance.PlaySFX("Audio/SFX/" + sfxName);
	}


}
