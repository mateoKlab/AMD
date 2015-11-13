using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Bingo;

public class BattleMenuModel : Model
{
	public Action<List<FighterData>> OnFightersSet;

	private List<FighterData> _fighters;
	
	public List<FighterData> fighters {
		get { return _fighters; }
		
		set {
			_fighters = value;
			if (OnFightersSet != null) {
				OnFightersSet (_fighters);
			}
		}
	}
	
}
