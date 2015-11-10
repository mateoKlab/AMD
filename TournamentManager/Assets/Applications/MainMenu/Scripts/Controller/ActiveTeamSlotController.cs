using UnityEngine;
using System.Collections;
using Bingo;

public class ActiveTeamSlotController : Controller
{
	private TroopController troopOnSlot;

	private bool isSlotOccupied()
	{
		// If a slot has no child, it means it's empty.
		return transform.childCount > 0;
	}

	public void SetTroopOnSlot(GameObject selectedTroop) {
		if(!isSlotOccupied())
		{
			troopOnSlot = selectedTroop.GetComponent<TroopController>();
			troopOnSlot.isAnActiveTroop = true;
			selectedTroop.transform.SetParent(transform);
			selectedTroop.transform.localPosition = Vector2.zero;
			//Debug.LogError("SetTroopOnSlot");
		}
	}


}
