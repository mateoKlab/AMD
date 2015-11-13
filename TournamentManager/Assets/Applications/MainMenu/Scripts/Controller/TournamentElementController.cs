using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentElementController : Controller
{
	public void SetRankValue(int i) {
		((TournamentElementModel)model).rankValue = i;
		((TournamentElementView)view).rankLabel.text = i.ToString();
	}

	public void SetStageData(StageData sData) {
		((TournamentElementModel)model).stageData = sData;
		((TournamentElementView)view).nameLabel.text = sData.name;
	}

	public void CheckIfStageIsUnlocked() {
		if (GameData.instance.playerData.tournamentProgress == app.model.GetComponentInChildren<TournamentModel>().tournamentMatchList.IndexOf(((TournamentElementModel)model).stageData))
		{	
			((TournamentElementView)view).fightButton.interactable = true;
		}
	}

	public void StartTournamentMatch() {
		GameData.instance.currentStage =  ((TournamentElementModel)model).stageData;
		Messenger.Send(MainMenuEvents.START_BATTLE, ((TournamentElementModel)model).stageData.id);
	}
}
