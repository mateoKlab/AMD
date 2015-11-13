using UnityEngine;
using System.Collections;
using Bingo;

public class ActiveTeamSlotController : Controller
{
	public int slotIndex;
	private TroopController troopOnSlot;

	private EditTeamController _editTeamController;
	protected EditTeamController editTeamController
	{
		get
		{
			if(_editTeamController == null)
				_editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();

			return _editTeamController;
		}
	}

	public override void Awake ()
	{
		base.Awake ();

		slotIndex = transform.GetSiblingIndex();
	}

	private bool isSlotOccupied()
	{
		// If a slot has no child, it means it's empty.
		return transform.childCount > 0;
	}

	public void SetTroopOnSlot(GameObject selectedTroop) {
		if(!isSlotOccupied())
		{
			if(selectedTroop.GetComponent<TroopController>() == null)
				return;

			troopOnSlot = selectedTroop.GetComponent<TroopController>();
			if(!editTeamController.IsWithinPartyCapacity(troopOnSlot.GetTroopCost()))
				return;

			troopOnSlot.SetTroopActive(slotIndex);
			selectedTroop.transform.SetParent(transform);
			selectedTroop.transform.localPosition = Vector2.zero;
		}
	}


}
