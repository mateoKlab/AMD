using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class FighterView : View {

	// TEMPORARY. Animation Frames.
	public Sprite attackSprite;
	public Sprite idleSprite;
	
	public Action<GameObject> OnCollideWithEnemy;

	// TEMPORARY.
	public void SetAttackSprite ()
	{
//		GetComponent<SpriteRenderer> ().sprite = attackSprite;
	}

	// TEMPORARY.
	public void SetIdleSprite ()
	{
//		GetComponent<SpriteRenderer> ().sprite = idleSprite;
	}

	public void SetSprite ()
	{
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer> ();
		string spriteName = (model as FighterModel).fighterData.spriteName;

		spriteRenderer.sprite = Resources.Load ("Sprites/" + spriteName, typeof(Sprite)) as Sprite;
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
