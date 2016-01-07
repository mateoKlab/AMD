using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopController : Controller<MainMenu, FighterModel, TroopView>
{
    public bool isAnActiveTroop;
	
    private Vector3 startPosition;
    private Transform startParent;
    private EditTeamController editTeamController;
    private int siblingIndex;

    public override void Awake()
    {
        base.Awake();

        editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();
      
    }

    void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void SetTroop(FighterData fighterData)
    {
        model.fighterData = fighterData;
		view.nameLabel.text = fighterData.name;
		view.classIcon.texture = Resources.Load("Sprites/ClassIcons/" + fighterData.fighterClass) as Texture;
    }

    public FighterData GetFighter()
    {
        return model.fighterData;
    }

    public void SetTroopActive(int slotIndex)
    {
        editTeamController.AddTroopOnTeam(model.fighterData);
    }

    public int GetTroopCost()
    {
        return model.cost;
    }
	
	public void ToggleState() {
		if (!editTeamController.model.activeTroops.Contains(model.fighterData) && (model.cost < GameData.instance.GetPartyCapacity() - editTeamController.GetPartyCost()) && editTeamController.model.activeTroops.Count < GameData.MAX_ACTIVE_FIGHTERS)
		{
			editTeamController.model.activeTroops.Add(model.fighterData);
		} else if (editTeamController.model.activeTroops.Contains(model.fighterData)) {
			editTeamController.model.activeTroops.Remove(model.fighterData);
		}
		editTeamController.view.SetCost(editTeamController.GetPartyCost(), GameData.instance.GetPartyCapacity());
		CheckState();
	}

	public void CheckState() {
		if (editTeamController.model.activeTroops.Contains(model.fighterData))
			view.stateLabel.text = "ACTIVE";
		else
			view.stateLabel.text = "IDLE";
	}

	public void DisplayTroopDetails() {
		editTeamController.ShowTroopDetails(model.fighterData);
	}

}
