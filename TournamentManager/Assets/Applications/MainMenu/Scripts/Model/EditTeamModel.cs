using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamModel : Model
{
	public List<TroopController> troops;

	public ActiveTeamSlotController[] activeTeamSlots;

	public ActiveTeamSlotController GetActiveTeamSlot(int index) {
		return activeTeamSlots[index];
	}
}
