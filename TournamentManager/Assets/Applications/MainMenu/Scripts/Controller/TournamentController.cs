using UnityEngine;
using System.Collections;
using Bingo;

public class TournamentController : Controller <MainMenu, TournamentModel, TournamentView>
{
	public void GoToBattleScene(params object[] args) 
	{
		Application.LoadLevel("BattleScene");
	}
}
