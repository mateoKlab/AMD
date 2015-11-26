using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Bingo;

public class BattleMenuController : Controller
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public BattleEndController battleEndController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public List<BattleMenuItemController> menuItems;

	private Dictionary<GameObject, BattleMenuItemController> menuItemDictionary;

	void Start ()
	{
		(model as BattleMenuModel).OnFightersSet += SetFighters;
		Messenger.AddListener (EventTags.FIGHTER_RECEIVED_DAMAGE, OnFighterReceivedDamage);
		Messenger.AddListener (EventTags.FIGHTER_KILLED, OnFighterKilled);
	}

	void OnDestroy ()
	{
		(model as BattleMenuModel).OnFightersSet -= SetFighters;
		Messenger.RemoveListener (EventTags.FIGHTER_RECEIVED_DAMAGE, OnFighterReceivedDamage);
		Messenger.RemoveListener (EventTags.FIGHTER_KILLED, OnFighterKilled);
	}

	public void SetFighters (List<GameObject> fighters)
	{
		DisableMenuItems ();
		menuItemDictionary = new Dictionary<GameObject, BattleMenuItemController> ();

		for (int i = 0; i < fighters.Count; i++) {
			menuItems[i].SetFighter (fighters[i]);
			menuItemDictionary.Add (fighters[i], menuItems[i]);
		}
	}

	void OnFighterReceivedDamage (params object[] args)
	{	
		FighterModel fighter = ((GameObject)args [1]).GetComponent<FighterModel> ();

		if (fighter.allegiance == FighterAlliegiance.Ally) {
			menuItemDictionary [fighter.gameObject].UpdateValues ();
		}
	}

	void OnFighterKilled (params object[] args)
	{
		GameObject fighter = (GameObject)args [0];
		if (fighter.GetComponent<FighterModel> ().allegiance == FighterAlliegiance.Ally) {
			menuItemDictionary [(GameObject)args [0]].ShowDeathIcon (true);
		}
	}

	void DisableMenuItems ()
	{
		foreach (BattleMenuItemController menuItem in menuItems) {
			menuItem.SetActive (false);
		}
	}
}
