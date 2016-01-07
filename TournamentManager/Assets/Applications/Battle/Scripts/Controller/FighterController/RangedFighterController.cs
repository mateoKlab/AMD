using UnityEngine;
using System.Collections;
using Bingo;

public class RangedFighterController : FighterController
{
	public float cooldown = 1.5f;
	public float nextAttackAllowed;

	private bool isOnCooldown = true;

	void Awake ()
	{
		base.Awake ();
		nextAttackAllowed = Time.time;
	}

	public override void OnAttack (Attack attackData)
	{
		base.OnAttack (attackData);

		RangedAttack (attackData);
	}

	void RangedAttack (Attack attackData)
	{
		//(app.controller as BattleController).OnRangedAttack (gameObject);
		((BattleController)app.controller).OnRangedAttack (attackData);
	}
}
