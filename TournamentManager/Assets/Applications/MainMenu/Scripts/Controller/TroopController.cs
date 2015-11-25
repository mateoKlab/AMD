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
        view.SetIcon(fighterData.normalIcon);
    }

    public FighterData GetFighter()
    {
        return model.fighterData;
    }

    public void SetTroopActive(int slotIndex)
    {
        editTeamController.AddTroopOnTeam(slotIndex, model.fighterData);
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
        canvasGroup.alpha = 1;
        startPosition = transform.position;
        startParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        gridLayoutGroup.enabled = false;
        siblingIndex = transform.GetSiblingIndex();
        transform.SetParent(editTeamController.transform, true);
        ((RectTransform)transform).position = new Vector3(((RectTransform)transform).position.x, ((RectTransform)transform).position.y, 0);
        transform.SetAsLastSibling();

        // Set troop as inactive as soon as it was dragged
        if (editTeamController.IsTroopActive(model.fighterData))
        {
            editTeamController.RemoveTroopOnTeam(model.fighterData);
        }
    }

    public void OnEndDrag()
    {
        canvasGroup.blocksRaycasts = true;

        if (transform.parent == editTeamController.transform)
        {
            transform.SetParent(startParent);
            ((RectTransform)transform).localPosition = Vector3.zero;
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
}
