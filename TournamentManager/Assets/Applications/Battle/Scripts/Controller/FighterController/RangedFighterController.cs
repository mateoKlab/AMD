using UnityEngine;
using System.Collections;
using Bingo;

// TODO: Rename to MageFighterController.
public class RangedFighterController : FighterController
{
	public float cooldown = 1.5f;

	public GameObject orb;


	// TEMP.
	// The sprite/object where the projectile should spawn.s
	public GameObject projectileSource;


	void Awake ()
	{
		base.Awake ();
	}

	protected override void StartAttack ()
	{
		base.StartAttack ();
	}

	public override void OnAttack (Attack attackData)
	{
		base.OnAttack (attackData);

		RangedAttack (attackData);

		orb.SetActive (false);
	}
	
	protected override void OnAttackEnded ()
	{
		base.OnAttackEnded ();

		// Turn on orb.
		orb.SetActive (true);
	}

	void RangedAttack (Attack attackData)
	{
		Vector3 position = projectileSource.transform.position;
//		position.x += 2.0f;

		((BattleController)app.controller).OnRangedAttack (attackData, position);
	}
}
