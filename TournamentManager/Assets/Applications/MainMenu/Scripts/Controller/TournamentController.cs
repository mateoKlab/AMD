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
		Messenger.Send(MainMenuEvents.START_BATTLE, GameData.instance.currentStage.id);
	}
}
