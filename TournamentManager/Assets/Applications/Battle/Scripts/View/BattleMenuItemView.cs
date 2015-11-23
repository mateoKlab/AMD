using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemView : View
{
	public Text fighterName;
	public Image hpSlider;
	public Image deathIcon;

	void Start ()
	{

	}

	public void UpdateValues ()
	{
		FighterData fighterData = (model as BattleMenuItemModel).fighter.GetComponent <FighterModel> ().fighterData;

		fighterName.text = fighterData.name;

		int currentHP = fighterData.HP;
		int maxHP	  = fighterData.maxHP;

		float hpFill = (float)currentHP / (float)maxHP;

		// TODO: Animate.. Gradually decrease fill amount.
		hpSlider.fillAmount = hpFill;
	}

	public void ShowDeathIcon (bool enabled)
	{
		deathIcon.gameObject.SetActive (enabled);
	}
}
