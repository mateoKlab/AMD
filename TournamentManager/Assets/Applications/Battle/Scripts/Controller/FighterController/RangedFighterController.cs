using UnityEngine;
using System.Collections;
using Bingo;

public class RangedFighterController : FighterController
{
	public float cooldown = 1.5f;

	private bool isOnCooldown = true;

	// TEMP.
	// The sprite/object where the projectile should spawn.s
	public GameObject projectileSource;


	void Awake ()
	{
		base.Awake ();
	}

	public override void OnAttack (Attack attackData)
	{
		base.OnAttack (attackData);

		RangedAttack (attackData);
	}

	void RangedAttack (Attack attackData)
	{
		((BattleController)app.controller).OnRangedAttack (attackData, projectileSource.transform.position);
	}
}
