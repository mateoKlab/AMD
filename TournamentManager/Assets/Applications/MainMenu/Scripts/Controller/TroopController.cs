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
	
	void Start() {
		canvasGroup = GetComponent<CanvasGroup>();
		editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();
		teamPanel = GameObject.Find("TeamPanel").transform;
		gridLayoutGroup = teamPanel.GetComponent<GridLayoutGroup>();
	}

	public void SetTroop(FighterData fighterData)
	{
		model.fighterData = fighterData;
	}

	public FighterData GetTroop()
	{
		return model.fighterData;
	}

	public void SetActiveTroopIndex(int index)
	{
		model.activeTroopIndex = index;
	}

	public void OnPointerEnter()
	{
		editTeamController.ShowTroopDetails(model.fighterData);
	}

	public void OnBeginDrag()
	{
		startPosition = transform.position;
		startParent = transform.parent;
		canvasGroup.blocksRaycasts = false;
		gridLayoutGroup.enabled = false;
		siblingIndex = transform.GetSiblingIndex();
		transform.SetParent(editTeamController.transform, true);
		transform.SetAsLastSibling();

		// Set troop as inactive as soon as it was dragged
		model.activeTroopIndex = -1;
	}

	public void OnEndDrag() 
	{
		canvasGroup.blocksRaycasts = true;

		if(transform.parent == editTeamController.transform)
		{
			transform.SetParent(startParent);
		}

		if(startParent == teamPanel)
		{
			transform.SetSiblingIndex(siblingIndex);
		}
		else
		{
			transform.SetAsLastSibling();
		}
		
		gridLayoutGroup.enabled = true;
		//Debug.LogError("TroopController OnEndDrag");
	}

	// If a troop being dragged is dropped over this troop, return that troop to team panel
	public void OnDrop(GameObject selectedObject) 
	{
		//Debug.LogError("EditTeamView OnDrop");
		editTeamController.ReturnTroopFromSlotToTeamPanel(selectedObject);
	}
}
