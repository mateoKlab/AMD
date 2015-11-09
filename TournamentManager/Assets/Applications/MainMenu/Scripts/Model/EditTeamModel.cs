using UnityEngine;
using System.Collections;
using Bingo;

public class EditTeamModel : Model
{
	public ActiveTeamSlotController[] activeTeamSlots = new ActiveTeamSlotController[5];

	public ActiveTeamSlotController GetActiveTeamSlot(int index) {
		return activeTeamSlots[index];
	}
}
