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
		//((TournamentElementView)view).fightButton.
	}

	public void CheckIfStageIsUnlocked() {
		if (GameData.Instance.PlayerData.unlockedStages.Contains(((TournamentElementModel)model).stageData.id)) {
			((TournamentElementView)view).fightButton.interactable = true;
		}
	}


}
