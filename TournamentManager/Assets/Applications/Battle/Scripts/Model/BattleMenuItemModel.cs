using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class BattleMenuItemModel : Model
{
	public Action <FighterData> OnFighterSet;

	private FighterData _fighterData;

	public FighterData fighterData {
		get { return _fighterData; }

		set {
			_fighterData = value;

			if (OnFighterSet != null) {
				OnFighterSet (_fighterData);
			}
		}
	}
}
