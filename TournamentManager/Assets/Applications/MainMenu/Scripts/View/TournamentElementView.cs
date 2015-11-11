using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class TournamentElementView : View
{
	public Text rankLabel;
	public Text nameLabel;
	public Button fightButton;

	public void OnClickFightButton() {
		GameData.Instance.currentStage =  ((TournamentElementModel)model).stageData;
		Messenger.Send(MainMenuEvents.START_BATTLE, ((TournamentElementModel)model).stageData.id);
	}
}
