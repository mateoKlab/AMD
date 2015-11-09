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

		FighterData fighter = ((FighterModel)GetComponent<Model> ()).FighterData;

		Debug.Log ("ATTACK: " + fighter.ATK.ToString());
		return new Attack (fighter.ATK, 1.0f, AttackType.Melee, gameObject);
	}

	// TEMPORARY.
	void AttackAnimation ()
	{


	}

	public void OnReceiveAttack (Attack attack)
	{
		// TODO: STUFF.
		// Calculate amount of knockback.
		// Calculate damage based on armor, if any.
		// Calculate skill effects, evade, block, etc.
		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;

		GetComponent<Rigidbody2D> ().AddForce (new Vector2 (5.0f * -moveDirection, 5.0f), ForceMode2D.Impulse);

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

}
