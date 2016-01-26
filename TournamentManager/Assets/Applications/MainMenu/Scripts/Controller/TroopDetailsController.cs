using UnityEngine;
using System.Collections;
using Bingo;

public class TroopDetailsController : Controller
{
    public void SetTroopDetails(FighterData fighterData)
    {
        ((TroopDetailsView) view).SetName(fighterData.name);
        ((TroopDetailsView) view).SetAtk(fighterData.ATK);
        ((TroopDetailsView) view).SetHP(fighterData.HP);
        ((TroopDetailsView) view).SetCost(fighterData.cost);
        ((TroopDetailsView) view).SetSprite(fighterData.fighterClass, fighterData.skinData);
    }
}
