using UnityEngine;
using System.Collections;
using Bingo;

public class ActiveTeamSlotController : Controller
{
	private GameObject troopOnSlot {
		get {
			if(transform.childCount > 0)
			{
				return transform.GetChild(0).gameObject;
			}
			return null;
		}
	}

	public void SetTroopOnSlot(GameObject selectedTroop) {
		// If a slot has no child, it means it's empty. Put troop on this slot.
		if(!troopOnSlot)
		{
			selectedTroop.transform.SetParent(transform);
			selectedTroop.transform.localPosition = Vector2.zero;
			Debug.LogError("SetTroopOnSlot");
		}
	}


}
