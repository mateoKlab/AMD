using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopDetailsView : View
{
	public Text nameText;
	public Text atkText;
	public Text hpText;
	public Text costText;
    public FighterSpriteController troopSprite;

    public void SetName(string name)
    {
       	if (nameText != null)
		{
			nameText.text = name;
		}
    }

    public void SetAtk(float atk)
    {
		if (atkText != null) 
		{
			atkText.text = Mathf.RoundToInt(atk).ToString("n0");
		}

    }

    public void SetHP(float hp)
    {
		if (hpText != null) {
			hpText.text = Mathf.RoundToInt(hp).ToString("n0");
		}       
    }

    public void SetCost(int cost)
    {
		if (costText != null) 
		{
			costText.text = cost.ToString("n0");
		}
    }

    public void SetSprite(FighterSkinData skinData)
    {
		if (troopSprite != null) {
			// TODO building sprite everytime feels really slow, refactor later
			troopSprite.SetFighterSkin(skinData);
			// 9162166868
		}
    }

}
