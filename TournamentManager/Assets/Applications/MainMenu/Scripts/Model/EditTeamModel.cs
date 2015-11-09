using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamModel : Model
{
	public List<TroopController> troops;

	public ActiveTeamSlotController[] activeTeamSlots = new ActiveTeamSlotController[5];

	public ActiveTeamSlotController GetActiveTeamSlot(int index) {
		return activeTeamSlots[index];
	}
}
