using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamController : Controller<MainMenu>
{
	private Transform activeTeamPanel;
	private Transform teamPanel;

	public override void Awake ()
	{
		base.Awake ();

		activeTeamPanel = transform.FindChild("ActiveTeamPanel");
		teamPanel = transform.FindChild("TeamPanel");
	}

	public void ShowEditTeam()
	{
		gameObject.SetActive(true);
	}

	public void HideEditTeam()
	{
		app.controller.EnableMainMenuItems(true);
		gameObject.SetActive(false);
	}

	public void ReturnTroopFromSlotToTeamPanel(GameObject troop) 
	{
		troop.transform.SetParent(teamPanel);
		troop.transform.SetAsLastSibling();
	}

	public bool CheckIfTroopChildOfTeamPanel(GameObject troop) 
	{
		return teamPanel.IsChildOf(teamPanel);
	}

}
