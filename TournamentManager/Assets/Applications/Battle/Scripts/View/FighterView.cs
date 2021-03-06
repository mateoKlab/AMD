﻿using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class FighterView : View {

	public FighterSpriteController fighterSprite;
	
	public Action<GameObject> OnCollideWithEnemy;
	public Action<GameObject> OnEnemyInRange;
	public Action<GameObject> OnEnemyExitRange;


	private Animator animator;

	void Awake ()
	{
		// Cache animator component.
		animator = fighterSprite.GetComponent<Animator> ();
	}

	public void SetFighterSkin (FighterSkinData skinData)
	{	
		fighterSprite.SetFighterSkin (skinData);
	}

	// Target Detection collider is set as trigger. 
	// Physics layering prevents triggering for allied units.
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Units) {

			OnEnemyInRange (other.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Units) {
			OnEnemyExitRange (other.gameObject);
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

//		if (coll.gameObject.tag == Tags.Units) {
//			OnCollideWithEnemy(coll.gameObject);
//		}

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
