using UnityEngine;
using System.Collections;
using Bingo;

public class RangedFighterController : FighterController
{
	public float cooldown = 1.5f;
	public float nextAttackAllowed;

	private bool isOnCooldown = true;

	void Start ()
	{
		base.Start ();
		nextAttackAllowed = Time.time;
	}

	void FixedUpdate() 
	{
		if (Time.time > nextAttackAllowed + cooldown) {
			Attack ();
		}
	}

	void Attack ()
	{
		nextAttackAllowed = Time.time + cooldown;

		//(app.controller as BattleController).OnRangedAttack (gameObject);
		((BattleController)app.controller).OnRangedAttack (gameObject);
	}
}
