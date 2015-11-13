using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Bingo;

public class BattleMenuController : Controller
{
	public List<BattleMenuItemController> menuItems;

	private Dictionary<FighterData, BattleMenuItemController> menuItemDictionary;

	void Start ()
	{
		(model as BattleMenuModel).OnFightersSet += SetFighters;
		Messenger.AddListener (EventTags.FIGHTER_RECEIVED_DAMAGE, OnFighterReceivedDamage);
	}

	void OnDestroy ()
	{
		(model as BattleMenuModel).OnFightersSet -= SetFighters;
		Messenger.RemoveListener (EventTags.FIGHTER_RECEIVED_DAMAGE, OnFighterReceivedDamage);
	}

	public void SetFighters (List<FighterData> fighters)
	{
		DisableMenuItems ();
		menuItemDictionary = new Dictionary<FighterData, BattleMenuItemController> ();

		for (int i = 0; i < fighters.Count; i++) {
			menuItems[i].SetFighter (fighters[i]);
			menuItemDictionary.Add (fighters[i], menuItems[i]);
		}
	}

	void OnFighterReceivedDamage (params object[] args)
	{	
		FighterModel fighter = ((GameObject)args [1]).GetComponent<FighterModel> ();

		if (fighter.allegiance == FighterModel.FighterAlliegiance.Ally) {
			menuItemDictionary [fighter.fighterData].UpdateValues ();
		}
	}

	void DisableMenuItems ()
	{
		foreach (BattleMenuItemController menuItem in menuItems) {
			menuItem.SetActive (false);
		}
	}
}
