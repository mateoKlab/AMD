using UnityEngine;
using System.Collections;
using Bingo;

public class TroopDetailsController : Controller
{
	public void SetTroopDetails(FighterData fighterData)
	{
		((TroopDetailsView)view).SetName(fighterData.name);
		((TroopDetailsView)view).SetAtk(fighterData.ATK);
		((TroopDetailsView)view).SetHP(fighterData.HP);
	}
}
