using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class ProjectileModel : Model
{
	public Action<GameObject> OnFighterSet;

	private GameObject _fighter;
	public GameObject fighter {
		get { return _fighter; }
		set {
			_fighter = value;

			if (OnFighterSet != null) {
				OnFighterSet (_fighter);
			}
		}
	}
}
