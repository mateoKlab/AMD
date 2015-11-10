using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopController : Controller
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
		gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
		editTeamController = transform.parent.parent.GetComponent<EditTeamController>();
		teamPanel = editTeamController.transform.FindChild("TeamPanel");
	}

	public void SetTroop(FighterData fighterData)
	{
		((TroopModel)model).SetFighterData(fighterData);
	}

	public void OnClick()
	{
		// TODO When clicked, update stats on display window
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
