using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using Bingo;

public class BattleMenuModel : Model
{   
	public Action<List<GameObject>> OnFightersSet;

	private List<GameObject> _fighters;
	
	public List<GameObject> fighters {
		get { return _fighters; }
		
		set {
			_fighters = value;
			if (OnFightersSet != null) {
				OnFightersSet (_fighters);
			}
		}
	}
	
}
