using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class FighterView : View {

	public FighterSpriteController fighterSprite;
	
	public Action<GameObject> OnCollideWithEnemy;


	// Temporary. TODO: Move to controller.
	public void AnimateAttack ()
	{
		fighterSprite.GetComponent<Animator> ().SetTrigger ("Attack");
	}

	public void SetSprite ()
	{	
		SpriteBuilder.instance.BuildSprite (fighterSprite);

	
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
