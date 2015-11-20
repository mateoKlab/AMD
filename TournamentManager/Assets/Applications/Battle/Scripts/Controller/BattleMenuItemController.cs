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

	public void SetFighter (GameObject fighter)
	{
		(model as BattleMenuItemModel).fighter = fighter;
	}

	public void SetActive (bool active)
	{
		gameObject.SetActive (active);
	}

	public void UpdateValues ()
	{
		(view as BattleMenuItemView).UpdateValues ();
	}

	void OnFighterSet ()
	{
		gameObject.SetActive (true);
		(view as BattleMenuItemView).UpdateValues ();
	}



}
