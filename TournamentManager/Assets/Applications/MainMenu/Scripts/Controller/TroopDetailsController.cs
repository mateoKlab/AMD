using UnityEngine;
using System.Collections;
using Bingo;

public class TroopDetailsController : Controller <MainMenu, TroopDetailsModel, TroopDetailsView>
{
    public void SetTroopDetails(FighterData fighterData)
    {
        view.SetName(fighterData.name);
		view.SetClass(fighterData.fighterClass.ToString());
		view.SetAtk(fighterData.ATK);
		view.SetHP(fighterData.HP);
		view.SetCost(fighterData.cost);
		view.SetSprite(fighterData.fighterClass, fighterData.skinData);
    }
}
