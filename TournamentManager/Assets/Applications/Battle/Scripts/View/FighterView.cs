using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class FighterView : View {

	public FighterSpriteController fighterSprite;
	
	public Action<GameObject> OnCollideWithEnemy;

	private Animator animator;

	void Start ()
	{
		// Cache animator component.
		animator = fighterSprite.GetComponent<Animator> ();
	}

	public void AnimateAttack ()
	{
		// Don't transition to attack while being hit.
		if (animator.IsInTransition (0) && animator.GetCurrentAnimatorStateInfo (0).IsName ("Hit")) {
			// Don't do it!
		} else {
			fighterSprite.GetComponent<Animator> ().SetBool ("Attack", true);
		}
	}

	public void AnimateHit ()
	{
		// Interrupt and cancel other animations when getting hit.
		animator.SetBool ("Attack", false);
		animator.SetBool ("Hit", true);
	}

	public void SetFighterSkin (FighterSkinData skinData)
	{	
		fighterSprite.SetFighterSkin (skinData);
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//Test
		if (other.gameObject.tag == Tags.Units) {
			AnimateAttack ();
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// TEST. TODO: use Tags.
		if (coll.gameObject.name == "Ground") 
		{
			// Enable Movement.
			((FighterController)controller).OnGroundEnter();
		}

		if (coll.gameObject.tag == Tags.Units) {
			OnCollideWithEnemy(coll.gameObject);
		}

	}

	void OnCollisionExit2D (Collision2D coll)
	{
		// TEST. TODO: use Tags.
		if (coll.gameObject.name == "Ground") 
		{
			// Disable Movement.
			((FighterController)controller).OnGroundExit();
		}
		
	}
}
