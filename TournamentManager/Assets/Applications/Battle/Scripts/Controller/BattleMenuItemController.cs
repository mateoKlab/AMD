using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemController : Controller <Battle, BattleMenuItemModel, BattleMenuItemView>
{

	void Awake ()
	{
		model.OnFighterSet += OnFighterSet;
	}

	public void SetFighter (GameObject fighter)
	{
		model.fighter = fighter;

//		(model as BattleMenuItemModel).fighterData = fighter;
//		(model as BattleMenuItemModel).fighterData.HP = (model as BattleMenuItemModel).fighterData.maxHP;
//		UpdateValues();
	}

	public void SetActive (bool active)
	{
		gameObject.SetActive (active);
	}

	public void UpdateHP ()
	{
		view.UpdateHP ();
	}

	public void ShowDeathIcon (bool enabled)
	{
		view.ShowDeathIcon (enabled);
	}

	void OnFighterSet ()
	{
		gameObject.SetActive (true);
		view.InitializeValues ();
	}



}
