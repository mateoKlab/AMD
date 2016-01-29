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

	public FighterSpriteController warriorPortrait;
	public FighterSpriteController magePortrait;

    public override void Awake()
    {
        base.Awake();

        editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();
      
    }

	public void OnEnable() {
		if (model.fighterData != null) 
		{
			SetPortrait (model.fighterData.fighterClass, model.fighterData.skinData);
		}

	}

    void Start()
    {
        transform.localScale = Vector3.one;
    }

    public void SetTroop(FighterData fighterData)
    {
        model.fighterData = fighterData;
		view.nameLabel.text = fighterData.name;
		view.lvlLabel.text = "Lvl " + fighterData.level;
		view.classIcon.texture = Resources.Load("Sprites/ClassIcons/" + fighterData.fighterClass) as Texture;

//		GetComponentInChildren<FighterSpriteController>().SetFighterSkin(fighterData.skinData);

		SetPortrait (fighterData.fighterClass, fighterData.skinData);
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
			SoundManager.instance.PlayUISFX("Audio/SFX/Add Character");
			editTeamController.AddTroopOnTeam(model.fighterData);
		} else if (GameData.instance.playerData.currentParty.fighters.Contains (model.fighterData.id)) {
			SoundManager.instance.PlayUISFX("Audio/SFX/Remove Character");
			editTeamController.RemoveTroopOnTeam(model.fighterData);
		}
		editTeamController.view.SetCost(GameData.instance.playerData.currentParty.currentCost/*editTeamController.GetPartyCost()*/, GameData.instance.playerData.partyCapacity);

		CheckState();
		DisplayTroopDetails();
	}

	public void CheckState() {
		if (GameData.instance.playerData.currentParty.fighters.Contains (model.fighterData.id))
			view.stateLabel.text = "Active";
		else
			view.stateLabel.text = "Idle";
	}

	public void DisplayTroopDetails() {
		//editTeamController.SetSelectedTroop(model.fighterData);
		editTeamController.ShowTroopDetails(model.fighterData);
	}

	private void SetPortrait (Class fighterClass, FighterSkinData skinData)
	{
		warriorPortrait.gameObject.SetActive (false);
		magePortrait.gameObject.SetActive (false);

		switch (fighterClass) {
		case Class.Warrior:
		{
			warriorPortrait.SetFighterSkin (skinData);
			warriorPortrait.gameObject.SetActive (true);
			break;
		}
			
		case Class.Mage:
		{

			magePortrait.SetFighterSkin (skinData);
			magePortrait.gameObject.SetActive (true);
			break;
		}
			
		}
	}

}
