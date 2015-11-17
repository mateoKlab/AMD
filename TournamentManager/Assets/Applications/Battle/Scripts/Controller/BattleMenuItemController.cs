using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemController : Controller
{

	void Awake ()
	{
		(model as BattleMenuItemModel).OnFighterSet += OnFighterSet;
	}

	public void SetFighter (FighterData fighter)
	{
		(model as BattleMenuItemModel).fighterData = fighter;
		(model as BattleMenuItemModel).fighterData.HP = (model as BattleMenuItemModel).fighterData.maxHP;
		UpdateValues();
	}

	public void SetActive (bool active)
	{
		gameObject.SetActive (active);
	}

	public void UpdateValues ()
	{
		(view as BattleMenuItemView).UpdateValues ();
	}

	void OnFighterSet (FighterData fighter)
	{
		gameObject.SetActive (true);
		(view as BattleMenuItemView).UpdateValues ();
	}



}
