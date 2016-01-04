using UnityEngine;
using System.Collections;
using Bingo;

public class MeleeFighterController : FighterController
{
	void FixedUpdate() 
	{
		base.FixedUpdate ();
	}

	public override void OnAttackEnded (Attack attack)
	{
		base.OnAttackEnded (attack);

		if (attack.attackTarget != null) {
			((BattleController)app.controller).OnMeleeAttack (attack);
		}

	}

	public void OnGroundEnter ()
	{
		((FighterModel)model).onGround = true;
	}
	
	public void OnGroundExit ()
	{
		((FighterModel)model).onGround = false;
	}
}
