using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Bingo;

public class EditTeamController : Controller<MainMenu, EditTeamModel, EditTeamView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public VerticalLayoutGroup content;
    private Transform teamPanel;
    private GameObject troopPrefab;

    private GameData _gameData;
    protected GameData gameData
    {
        get
        {
            return (_gameData ?? (_gameData = GameData.instance));
        }
    }

    public override void Awake()
    {
		base.Awake();
        teamPanel = transform.Find("TeamPanelScrollView/TeamPanel");
        //model.activeTeamSlots = activeTeamPanel.GetComponentsInChildren<ActiveTeamSlotController>();

		for (int i = 0; i < gameData.playerData.fighterCapacity; i++)
		{
			GameObject slotPrefab = Instantiate(Resources.Load("Prefabs/PartyCostSlot", typeof(GameObject))) as GameObject;
			slotPrefab.transform.SetParent(view.partyCostBar.transform, false);
			view.slots.Add(slotPrefab.GetComponent<RawImage>());
		}
    }

    void OnEnable()
    {
        if (model.troops.Count != gameData.GetFightersOwned().Count)
            StartCoroutine("LoadAllTroops");
    }

    public void ShowEditTeam()
    {
        Input.multiTouchEnabled = false;
        gameObject.SetActive(true);
    }

    public void HideEditTeam()
    {
        // There were no active troops, do not allow player to leave edit team screen
        // TODO Add popup here or disable close button
        if (gameData.playerData.currentParty.currentCost <= 0)
        {
            Debug.LogError("Atleast 1 troop in party is required, please assign at least 1 before exiting");
            return;
        }

        SaveTeam();
        Input.multiTouchEnabled = true;
        app.controller.EnableMainMenuItems(true);
        gameObject.SetActive(false);

		Debug.LogError ("Active Troops:" + model.activeTroops.Count);
    }

    private void SaveTeam()
    {
        for(int i = 0; i < model.activeTroops.Count; i++)
        {
            gameData.SetFighterOnActiveParty(model.activeTroops[i], i);
        }
        gameData.SavePlayerData();
    }

    private IEnumerator LoadAllTroops()
    {
        DeleteAllTroops();
        yield return new WaitForEndOfFrame();
        troopPrefab = Resources.Load("Prefabs/Troop") as GameObject;
        yield return StartCoroutine("LoadTroops");
        UpdateTroopStatus ();
        yield return new WaitForEndOfFrame();

        view.SetCost(gameData.playerData.currentParty.currentCost, gameData.playerData.partyCapacity);
    }

    private void DeleteAllTroops()
    {
        // TODO refactor this shit, too slow and too expensive
        // Destroy troops on TeamPanel
        TroopController[] currentTroops = teamPanel.GetComponentsInChildren<TroopController>();
        for (int i = 0; i < currentTroops.Length; i++)
        {
            DestroyObject(currentTroops[i].gameObject);
        }

//        // Destroy troops on ActiveTeamPanel
//        for (int i = 0; i < model.activeTeamSlots.Count; i++)
//        {
//            if (model.activeTeamSlots[i].transform.childCount > 0)
//            {
//                DestroyObject(model.activeTeamSlots[i].transform.GetChild(0).gameObject);
//            }
//        }

        model.troops.Clear();
        for (int i = 0; i < model.activeTroops.Count; i++)
        {
            model.activeTroops[i] = null;
        }
    }

    private IEnumerator LoadTroops()
    {
        List<FighterData> fighters = gameData.GetFightersOwned();
        foreach (FighterData fighter in fighters)
        {
            GameObject go = Instantiate(troopPrefab);
//            go.AddComponent<FighterModel>();
            go.transform.SetParent(teamPanel);
            ((RectTransform)go.transform).localPosition = Vector3.zero;

            TroopController tc = go.GetComponent<TroopController>();
            model.troops.Add (tc);
            tc.SetTroop (fighter);
        }

		// Resize scrollable background based on number of elements
		float elementHeight = troopPrefab.GetComponent<LayoutElement>().minHeight + content.spacing;
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * fighters.Count + 1 + content.padding.right + content.padding.bottom);

        yield return new WaitForEndOfFrame();
        ShowTroopDetails(fighters[0]);
    }

	private void UpdateTroopStatus ()
	{
		foreach (TroopController troop in model.troops) {
			troop.CheckState ();
		}
	}

//    private void MoveActiveTroopsToActiveSlots()
//    {
//        for (int i = 0; i < model.troops.Count; i++)
//        {
//            if (gameData.CheckIfFighterActive(model.troops[i].GetFighter()))
//            {
//                //int index = gameData.GetActiveFighterIndexOnParty(model.troops[i].GetFighter());
//                // model.GetActiveTeamSlot(index).SetTroopOnSlot(model.troops[i].gameObject);
//                AddTroopOnTeam(model.troops[i].GetFighter());
//            }
//			model.troops[i].CheckState();
//        }
//    }

    public void ReturnTroopFromSlotToTeamPanel(GameObject troop)
    {
        if (troop.GetComponent<TroopController>() == null)
            return;

        troop.transform.SetParent(teamPanel);
        troop.transform.SetAsLastSibling();
    }

    public void ShowTroopDetails(FighterData fighter)
    {
        troopDetailsController.SetTroopDetails(fighter);
    }

    public void AddTroopOnTeam(FighterData troop)
    {
		if (!model.activeTroops.Contains (troop)) {
			model.activeTroops.Add (troop);

			// new. TODO: cleanup, migrate/deprecate old code.
			gameData.playerData.AddToParty (troop);
			Debug.Log ("ADD: " + gameData.playerData.currentParty.currentCost + " / " + gameData.playerData.partyCapacity);

		}


		view.SetCost(gameData.playerData.currentParty.currentCost, gameData.playerData.partyCapacity);
	}

    public void RemoveTroopOnTeam(FighterData fd)
    {
        for(int i = 0; i < model.activeTroops.Count; i++)
        {
            if(model.activeTroops[i] != null && model.activeTroops[i].id == fd.id)
            {
                model.activeTroops[i] = null;
                break;
            }
        }

		gameData.playerData.RemoveFromParty (fd);
		view.SetCost(gameData.playerData.currentParty.currentCost, gameData.playerData.partyCapacity);
	}

    public bool IsTroopActive(FighterData fd)
    {
        for(int i = 0; i < model.activeTroops.Count; i++)
        {
            if(model.activeTroops[i] != null && model.activeTroops[i].id == fd.id)
            {
                return true;
            }
        }

        return false;
    }

    public int GetPartyCost()
    {
        int cost = 0;
        for (int i = 0; i < model.activeTroops.Count; i++)
        {
            if (model.activeTroops[i] != null)
            {
                cost += model.activeTroops[i].cost;
            }
        }
        return cost;
    }

    public bool IsWithinPartyCapacity(int troopCost)
    {
		return ((gameData.playerData.currentParty.currentCost + troopCost) <= gameData.playerData.fighterCapacity);
    }
}
