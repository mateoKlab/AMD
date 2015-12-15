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
		view.enemyCountLabel.text = "Number of Enemies: " + mPointModel.missionPointData.enemies.Count;
		view.goldRewardLabel.text = "Gold Reward: " + mPointModel.missionPointData.goldReward;
		view.expRewardLabel.text = "Exp Reward: " + mPointModel.missionPointData.xpReward;
		GetComponent<Animator>().SetTrigger("TransitionIn");
	}

	
	public void StartMissionMatch() {
		GameData.instance.currentStage =  currentMissionPoint.missionPointData;
		Messenger.Send(MainMenuEvents.START_BATTLE, currentMissionPoint.missionPointData.id);
	}

	public void TransitionOut() {
		StartCoroutine(TransitionOutCoroutine());
	}

	IEnumerator TransitionOutCoroutine() {
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}
}
