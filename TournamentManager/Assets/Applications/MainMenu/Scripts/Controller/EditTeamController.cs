using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamController : Controller<MainMenu>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	private EditTeamModel model;

	private Transform activeTeamPanel;
	private Transform teamPanel;
	private GameObject troopPrefab;

	private PlayerData _playerData;
	private PlayerData playerData
	{
		get
		{
			return (_playerData ?? (_playerData = GameData.Instance.PlayerData));
		}
	}

	public override void Awake ()
	{
		base.Awake ();

		model = (EditTeamModel)base.model;

		activeTeamPanel = transform.FindChild("ActiveTeamPanel");
		teamPanel = transform.FindChild("TeamPanel");

		model.activeTeamSlots = activeTeamPanel.GetComponentsInChildren<ActiveTeamSlotController>();
	}

	void OnEnable() {
		if(model.troops.Count != playerData.fightersOwned.Count)
		{
			DeleteAllTroops();
			StartCoroutine("LoadTroops");
			MoveActiveTroopsToActiveSlots();
		}
	}

	public void ShowEditTeam()
	{
		Input.multiTouchEnabled = false;
		gameObject.SetActive(true);
	}

	public void HideEditTeam()
	{
		playerData.Save();
		Input.multiTouchEnabled = true;
		app.controller.EnableMainMenuItems(true);
		gameObject.SetActive(false);
	}

	public IEnumerator LoadTroops()
	{
		troopPrefab = Resources.Load("Prefabs/Troop") as GameObject;
		
		List<FighterData> fighters = playerData.fightersOwned;
		for(int i = 0; i < fighters.Count; i++)
		{
			GameObject go = Instantiate(troopPrefab);
			go.transform.SetParent(teamPanel);
			TroopController tc = go.GetComponent<TroopController>();
			model.troops.Add(tc);
			tc.SetTroop(fighters[i]);
		}

		yield return new WaitForEndOfFrame();
		ShowTroopDetails(fighters[0]);
	}

	public void DeleteAllTroops()
	{
		// TODO refactor this shit, too slow and too expensive
		// Destroy troops on TeamPanel
		TroopController[] currentTroops = teamPanel.GetComponentsInChildren<TroopController>();
		for(int i = 0; i < currentTroops.Length; i++)
		{
			DestroyObject(currentTroops[i].gameObject);
		}
		// Destroy troops on ActiveTeamPanel
		for(int i = 0; i < model.activeTeamSlots.Length; i++)
		{
			if(model.activeTeamSlots[i].transform.childCount > 0)
			{
				DestroyObject(model.activeTeamSlots[i].transform.GetChild(0).gameObject);
			}
		}
	}

	public void MoveActiveTroopsToActiveSlots()
	{
		for(int i = 0; i < model.troops.Count; i++)
		{
			if(model.troops[i].GetTroop().activeTroopIndex > -1)
			{
				model.GetActiveTeamSlot(model.troops[i].GetTroop().activeTroopIndex).SetTroopOnSlot(model.troops[i].gameObject);
			}
		}
	}

	public void ReturnTroopFromSlotToTeamPanel(GameObject troop) 
	{
		troop.transform.SetParent(teamPanel);
		troop.transform.SetAsLastSibling();
	}

	public void ShowTroopDetails(FighterData fighter)
	{
		troopDetailsController.SetTroopDetails(fighter);
	}

}
