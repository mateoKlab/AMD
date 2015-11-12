using UnityEngine;
using System.Collections;
using Bingo;

public class FighterController : Controller {

	// Use this for initialization
	void Start () {
	
	}

	void FixedUpdate() 
	{
		if (!((FighterModel)model).onGround) {
			return;
		}

		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;
		transform.position = new Vector3 (transform.position.x + (0.05f * moveDirection) , transform.position.y, transform.position.z);
	}

	public Attack Attack ()
	{
		//TODO: Animate, Change State, etc.

		FighterData fighter = ((FighterModel)GetComponent<Model> ()).fighterData;

		StartCoroutine ("AttackAnimation");

		Debug.Log ("ATTACK: " + fighter.ATK.ToString());
		return new Attack (fighter.ATK, 1.0f, AttackType.Melee, gameObject);
	}

	// TEST. Temporary animation until SPINE.
	IEnumerator AttackAnimation ()
	{
		((FighterView)view).SetAttackSprite ();
		yield return new WaitForSeconds (0.6f);

		((FighterView)view).SetIdleSprite ();
	}

	public void OnReceiveAttack (Attack attack)
	{
		// TODO: STUFF.
		// Calculate skill effects, evade, block, etc.

		ReceiveDamage (attack);
		ReceiveKnockback (attack.knockback);

		//TEST
		OnGroundExit ();
	}

	public void OnCollideWithEnemy (Collision2D coll)
	{
		((BattleController)app.controller).OnUnitAttack (gameObject, coll.gameObject);
	}

	public void OnGroundEnter ()
	{
		((FighterModel)model).onGround = true;
	}


	public void OnGroundExit ()
	{
		((FighterModel)model).onGround = false;
	}


	private void ReceiveDamage (Attack attack)
	{
		Debug.Log (((FighterModel)model).fighterData.name + " received damage");

		FighterData defender = ((FighterModel)model).fighterData;
		FighterData attacker = attack.attackOrigin.GetComponent <FighterModel> ().fighterData;
		// Temporary Damage computation. TODO: Apply armor damage reduction effects.
		defender.HP -= attack.damage;

		if (defender.HP <= 0) {
			Debug.Log (defender.name + " died.");
			Messenger.Send (EventTags.FIGHTER_KILLED, defender, attacker);

			// TEST.
			Destroy (gameObject);
		}
	}

	private void ReceiveKnockback (float knockback)
	{
		// Temporary konckback. TODO: Apply knockback resistance/amount.
		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;
		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (5.0f * -moveDirection, 5.0f), ForceMode2D.Impulse);
	}
}
