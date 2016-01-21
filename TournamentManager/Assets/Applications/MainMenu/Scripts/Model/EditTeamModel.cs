using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class EditTeamModel : Model
{
	public List<TroopController> troops;
	public List<FighterData> activeTroops = new List<FighterData>();//FighterData[GameData.MAX_ACTIVE_FIGHTERS];
	public FighterData selectedTroop;
	//public List<ActiveTeamSlotController> activeTeamSlots =  new List<ActiveTeamSlotController>();
}
