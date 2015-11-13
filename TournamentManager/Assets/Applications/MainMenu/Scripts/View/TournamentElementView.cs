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
		((TournamentElementController)controller).StartTournamentMatch();
	}
}
