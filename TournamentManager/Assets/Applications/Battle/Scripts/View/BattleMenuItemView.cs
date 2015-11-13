using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemView : View
{
	public Text fighterName;
	public Image hpSlider;

	public void UpdateValues ()
	{
		fighterName.text = (model as BattleMenuItemModel).fighterData.name;

		int currentHP = (model as BattleMenuItemModel).fighterData.HP;
		int maxHP	  = (model as BattleMenuItemModel).fighterData.maxHP;

		float hpFill = (float)currentHP / (float)maxHP;

		// TODO: Animate.. Gradually decrease fill amount.
		hpSlider.fillAmount = hpFill;
	}
}
