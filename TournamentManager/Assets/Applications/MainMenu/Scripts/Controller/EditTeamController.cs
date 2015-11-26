using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamController : Controller<MainMenu, EditTeamModel, EditTeamView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    

    private Transform activeTeamPanel;
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

        activeTeamPanel = transform.Find("ActiveTeamPanel");
        teamPanel = transform.Find("TeamPanelScrollView/TeamPanel");
        model.activeTeamSlots = activeTeamPanel.GetComponentsInChildren<ActiveTeamSlotController>();
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
        if (GetPartyCost() <= 0)
        {
            Debug.LogError("Atleast 1 troop in party is required, please assign at least 1 before exiting");
            return;
        }

        SaveTeam();
        Input.multiTouchEnabled = true;
        app.controller.EnableMainMenuItems(true);
        gameObject.SetActive(false);
    }

    private void SaveTeam()
    {
        for(int i = 0; i < model.activeTroops.Length; i++)
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
        MoveActiveTroopsToActiveSlots();
        yield return new WaitForEndOfFrame();
        view.SetCost(GetPartyCost(), gameData.GetPartyCapacity());
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
        // Destroy troops on ActiveTeamPanel
        for (int i = 0; i < model.activeTeamSlots.Length; i++)
        {
            if (model.activeTeamSlots[i].transform.childCount > 0)
            {
                DestroyObject(model.activeTeamSlots[i].transform.GetChild(0).gameObject);
            }
        }

        model.troops.Clear();
        for (int i = 0; i < model.activeTroops.Length; i++)
        {
            model.activeTroops[i] = null;
        }
    }

    private IEnumerator LoadTroops()
    {
        List<FighterData> fighters = gameData.GetFightersOwned();
        for (int i = 0; i < fighters.Count; i++)
        {
            GameObject go = Instantiate(troopPrefab);
            go.AddComponent<FighterModel>();
            go.transform.SetParent(teamPanel);
            ((RectTransform)go.transform).localPosition = Vector3.zero;
            TroopController tc = go.GetComponent<TroopController>();
            model.troops.Add(tc);
            tc.SetTroop(fighters[i]);
        }

        yield return new WaitForEndOfFrame();
        ShowTroopDetails(fighters[0]);
    }
	
    private void MoveActiveTroopsToActiveSlots()
    {
        for (int i = 0; i < model.troops.Count; i++)
        {
            if (gameData.CheckIfFighterActive(model.troops[i].GetFighter()))
            {
                int index = gameData.GetActiveFighterIndexOnParty(model.troops[i].GetFighter());
                model.GetActiveTeamSlot(index).SetTroopOnSlot(model.troops[i].gameObject);
                AddTroopOnTeam(index, model.troops[i].GetFighter());
            }
        }
    }

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

    public void AddTroopOnTeam(int index, FighterData troop)
    {
        model.activeTroops[index] = troop;
        view.SetCost(GetPartyCost(), gameData.GetPartyCapacity());
    }

    public void RemoveTroopOnTeam(FighterData fd)
    {
        for(int i = 0; i < model.activeTroops.Length; i++)
        {
            if(model.activeTroops[i] != null && model.activeTroops[i].id == fd.id)
            {
                model.activeTroops[i] = null;
                break;
            }
        }

        view.SetCost(GetPartyCost(), gameData.GetPartyCapacity());
    }

    public bool IsTroopActive(FighterData fd)
    {
        for(int i = 0; i < model.activeTroops.Length; i++)
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
        for (int i = 0; i < model.activeTroops.Length; i++)
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
        return ((GetPartyCost() + troopCost) <= gameData.GetPartyCapacity());
    }
}
