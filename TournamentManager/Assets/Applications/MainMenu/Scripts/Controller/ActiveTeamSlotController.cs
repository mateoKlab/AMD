using UnityEngine;
using System.Collections;
using Bingo;

public class ActiveTeamSlotController : Controller
{
	public int slotIndex;
	private TroopController troopOnSlot;

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
			troopOnSlot = selectedTroop.GetComponent<TroopController>();
			troopOnSlot.SetActiveTroopIndex(slotIndex);
			selectedTroop.transform.SetParent(transform);
			selectedTroop.transform.localPosition = Vector2.zero;
			//Debug.LogError("SetTroopOnSlot");
		}
	}


}
