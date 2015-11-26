using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentElementController : Controller <MainMenu, TournamentElementModel, TournamentElementView>
{
	public void SetRankValue(int i) {
		model.rankValue = i;
		view.rankLabel.text = i.ToString();
	}

	public void SetStageData(StageData sData) {

		Debug.Log ("SET STAGE DATA");
		model.stageData = sData;
		view.nameLabel.text = sData.name;
	}

	public void CheckIfStageIsUnlocked() {
		if (GameData.instance.playerData.tournamentProgress == app.model.tournamentModel.tournamentMatchList.IndexOf(model.stageData))
		{	
			view.fightButton.interactable = true;
		}
	}

	public void StartTournamentMatch() {
		GameData.instance.currentStage =  model.stageData;
		Messenger.Send(MainMenuEvents.START_BATTLE, model.stageData.id);
	}
}
