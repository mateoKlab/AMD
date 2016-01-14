using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentController : Controller <MainMenu, TournamentModel, TournamentView>
{
	public void GoToBattleScene(params object[] args) 
	{
		Application.LoadLevel("BattleScene");
	}


	public void ShowStageDetails(StageData sData) {
		GameData.instance.currentStage = sData;

		view.stageNameLabel.text = sData.name;
		view.enemyCountLabel.text = "Number of Enemies: " + sData.enemies.Count;
		view.goldRewardLabel.text = "Gold Reward: " + sData.goldReward;
		view.expRewardLabel.text = "Exp Reward: " + sData.xpReward;
		view.stageDetailsPopUp.SetActive(true);
		//view.emblem.texture = Resources.Load<Texture>("Sprites/Emblems/" + sData.id);
		//view.emblem.rectTransform.sizeDelta = new Vector2(view.emblem.texture.width * 2, view.emblem.texture.height * 2);
	}

	public void CloseStageDetails() {
		view.stageDetailsPopUp.SetActive(false);
	}

	public void StartTournamentMatch() {
		app.controller.GoToBattleScene(GameData.instance.currentStage.id);
		//Messenger.Send(MainMenuEvents.START_BATTLE, GameData.instance.currentStage.id);
	}

	public void TransitionOut() {
		StartCoroutine(TransitionOutCoroutine());
	}
	
	IEnumerator TransitionOutCoroutine() {
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1.25f);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}
}
