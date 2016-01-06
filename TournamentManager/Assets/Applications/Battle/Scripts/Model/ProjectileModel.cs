using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class ProjectileModel : Model
{
	public Action<Attack> OnAttackDataSet;


	private Attack _attackData;
	public Attack attackData {
		get { return _attackData; }
		set {
			_attackData = value;

			if (OnAttackDataSet != null) {
				OnAttackDataSet (_attackData);
			}
		}
	}
}
