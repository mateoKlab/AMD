using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopController : Controller<MainMenu, TroopModel, TroopView>
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
		GetComponentInChildren<FighterSpriteController>().SetFighterSkin(fighterData.skinData);
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
        return model.fighterData.cost;
    }
	
	public void ToggleState() {

		if (!GameData.instance.playerData.currentParty.fighters.Contains (model.fighterData.id) && (model.fighterData.cost < GameData.instance.playerData.partyCapacity - editTeamController.GetPartyCost()) && GameData.instance.playerData.currentParty.fighters.Count < GameData.MAX_ACTIVE_FIGHTERS)
		{
			editTeamController.AddTroopOnTeam(model.fighterData);
		} else if (GameData.instance.playerData.currentParty.fighters.Contains (model.fighterData.id)) {
			editTeamController.RemoveTroopOnTeam(model.fighterData);
		}
		editTeamController.view.SetCost(GameData.instance.playerData.currentParty.currentCost/*editTeamController.GetPartyCost()*/, GameData.instance.playerData.partyCapacity);

		CheckState();
	}

	public void CheckState() {
		if (GameData.instance.playerData.currentParty.fighters.Contains (model.fighterData.id))
			view.stateLabel.text = "ACTIVE";
		else
			view.stateLabel.text = "IDLE";
	}

	public void DisplayTroopDetails() {
		//editTeamController.SetSelectedTroop(model.fighterData);
		editTeamController.ShowTroopDetails(model.fighterData);
	}

}
