using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopController : Controller<MainMenu, FighterModel, TroopView>
{
    public bool isAnActiveTroop;
	
    private Vector3 startPosition;
    private Transform startParent;
    private CanvasGroup canvasGroup;
    private GridLayoutGroup gridLayoutGroup;
    private EditTeamController editTeamController;
    private Transform teamPanel;
    private int siblingIndex;

    public override void Awake()
    {
        base.Awake();

        canvasGroup = GetComponent<CanvasGroup>();
        editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();
        teamPanel = GameObject.Find("TeamPanel").transform;
        gridLayoutGroup = teamPanel.GetComponent<GridLayoutGroup>();
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

    public void OnPointerEnter()
    {
        editTeamController.ShowTroopDetails(model.fighterData);
    }

    public void OnBeginDrag()
    {
		return;
        canvasGroup.alpha = 1;
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        gridLayoutGroup.enabled = false;
        siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(editTeamController.transform, true);
        transform.SetAsLastSibling();

        // Set troop as inactive as soon as it was dragged
        if (editTeamController.IsTroopActive(model.fighterData))
        {
            editTeamController.RemoveTroopOnTeam(model.fighterData);
        }
    }

    public void OnEndDrag()
    {
		return;
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == editTeamController.transform)
        {
            transform.SetParent(startParent);
        }

        if (startParent == teamPanel)
        {
            transform.SetSiblingIndex(siblingIndex);
        }
        else
        {
            transform.SetAsLastSibling();
        }
		
        gridLayoutGroup.enabled = true;
        canvasGroup.alpha = 1;
    }


    // If a troop being dragged is dropped over this troop, return that troop to team panel
    public void OnDrop(GameObject selectedObject)
    {
        editTeamController.ReturnTroopFromSlotToTeamPanel(selectedObject);
    }

	public void ToggleState() {
		if (!editTeamController.model.activeTroops.Contains(model.fighterData) && (model.cost < GameData.instance.GetPartyCapacity() - editTeamController.GetPartyCost()) && editTeamController.model.activeTroops.Count < GameData.MAX_ACTIVE_FIGHTERS)
		{
			editTeamController.model.activeTroops.Add(model.fighterData);
			view.stateLabel.text = "ACTIVE";
		} else if (editTeamController.model.activeTroops.Contains(model.fighterData)) {
			editTeamController.model.activeTroops.Remove(model.fighterData);
			view.stateLabel.text = "IDLE";
		}
		editTeamController.view.SetCost(editTeamController.GetPartyCost(), GameData.instance.GetPartyCapacity());
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
