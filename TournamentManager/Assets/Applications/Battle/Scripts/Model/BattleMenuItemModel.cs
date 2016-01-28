using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class BattleMenuItemModel : Model
{
	public Action OnFighterSet;
	public FighterData fData;

	private GameObject _fighter;

	public GameObject fighter {
		get { return _fighter; }

		set {
			_fighter = value;

			if (OnFighterSet != null) {
				OnFighterSet ();
			}
		}
	}
}
