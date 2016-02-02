using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Bingo;

public class BattleMenuController : Controller
{
	public List<BattleMenuItemController> menuItems;
	public List<EnemyHPBarScript> enemyHPItems;

	private Dictionary<GameObject, BattleMenuItemController> menuItemDictionary;
	private Dictionary<GameObject, EnemyHPBarScript> enemyHPBarDictionary;

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

	public void SetEnemies (List<GameObject> fighters)
	{
		enemyHPBarDictionary = new Dictionary<GameObject, EnemyHPBarScript> ();

		for (int i = 0; i < fighters.Count; i++) {
			enemyHPBarDictionary.Add (fighters[i], enemyHPItems[i]);
		}
	}


	void OnFighterReceivedDamage (params object[] args)
	{	
		FighterModel fighter = ((GameObject)args [1]).GetComponent<FighterModel> ();

		if (fighter.allegiance == FighterAlliegiance.Ally) {
			menuItemDictionary [fighter.gameObject].UpdateHP ();
		} else if (fighter.fighterData.HP > 0){
			enemyHPBarDictionary[fighter.gameObject].UpdateHP (fighter.gameObject, fighter.fighterData);
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
