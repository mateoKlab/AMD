using UnityEngine;
using System.Collections;
using Bingo;

public class TroopDetailsController : Controller <MainMenu, TroopDetailsModel, TroopDetailsView>
{
    public void SetTroopDetails(FighterData fighterData)
    {
        view.SetName(fighterData.name);
		view.SetLevel(fighterData.level);
		view.SetIcon(fighterData.fighterClass.ToString());
		view.SetClass(fighterData.fighterClass.ToString());
		view.SetAtk(fighterData.ATK);
		view.SetDef(fighterData.DEF);
		view.SetHP(fighterData.HP);
		view.SetCost(fighterData.cost);
		view.SetSprite(fighterData.fighterClass, fighterData.skinData);
    }

}
