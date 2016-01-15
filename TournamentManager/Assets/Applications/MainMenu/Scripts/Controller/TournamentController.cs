using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentController : Controller <MainMenu, TournamentModel, TournamentView>
{
	public void ShowStageDetails(StageData sData) {
		GameData.instance.currentStage = sData;

		view.stageNameLabel.text = sData.name;
		view.enemyCountLabel.text = "Number of Enemies: " + sData.enemies.Count;
		view.goldRewardLabel.text = "Gold Reward: " + sData.goldReward;
		view.expRewardLabel.text = "Exp Reward: " + sData.xpReward;
		view.stageDetailsPopUp.SetActive(true);

	}

//	public void CloseStageDetails() {
//		view.stageDetailsPopUp.SetActive(false);
//	}

	public void StartTournamentMatch() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		app.controller.GoToBattleScene(GameData.instance.currentStage.id);
	}

	public void TransitionOut() {
		StartCoroutine(TransitionOutCoroutine());
	}
	
	IEnumerator TransitionOutCoroutine() {
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1.25f);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}

	public void PlaySFXEvent(string sfxName) {
		SoundManager.instance.PlaySFX("Audio/SFX/" + sfxName);
	}

	public void PlayUISFXEvent(string sfxName) {
		SoundManager.instance.PlayUISFX("Audio/SFX/" + sfxName);
	}
}
