using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class TournamentElementView : View <MainMenu>
{
	public Text rankLabel;
	public Text nameLabel;
	public Button fightButton;

	public void OnClickDetailsButton() {
		app.controller.tournamentController.ShowStageDetails(((TournamentElementModel)model).stageData);
	}
}
