using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamController : Controller<MainMenu>
{
	private EditTeamModel model;

	private Transform activeTeamPanel;
	private Transform teamPanel;
	private GameObject troopPrefab;

	public override void Awake ()
	{
		base.Awake ();

		model = (EditTeamModel)base.model;

		activeTeamPanel = transform.FindChild("ActiveTeamPanel");
		teamPanel = transform.FindChild("TeamPanel");
	}

	void Start() {
		troopPrefab = Resources.Load("Prefabs/Troop") as GameObject;

		List<FighterData> fighters = GameData.Instance.PlayerData.fightersOwned;
		for(int i = 0; i < fighters.Count; i++)
		{
			GameObject go = Instantiate(troopPrefab);
			go.transform.SetParent(teamPanel);
			TroopController tc = go.GetComponent<TroopController>();
			model.troops.Add(tc);
			tc.SetTroop(fighters[i]);
		}
	}

	public void ShowEditTeam()
	{
		Input.multiTouchEnabled = false;
		gameObject.SetActive(true);
	}

	public void HideEditTeam()
	{
		Input.multiTouchEnabled = true;
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
