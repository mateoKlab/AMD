﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class FighterView : View {

	public FighterSpriteController fighterSprite;

	// Temp. Give cape its own controller to avoid clutter in main controller.
	public FighterSpriteController capeSprite;

	public Action<GameObject> OnCollideWithEnemy;
	public Action<GameObject> OnCollideWithEnemyExit;

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

	// TEMP.
	public void SetSpriteColor ()
	{
		StartCoroutine ("ChangeColor");
	}

	IEnumerator ChangeColor ()
	{
		foreach (FighterSpriteAttachment spriteAttachment in fighterSprite.spriteAttachments) {
			SpriteRenderer renderer = spriteAttachment.gameObject.GetComponent<SpriteRenderer> ();
			renderer.color = new Color32 (200,100,100,255);
		}

		yield return new WaitForSeconds (0.1f);

		foreach (FighterSpriteAttachment spriteAttachment in fighterSprite.spriteAttachments) {
			SpriteRenderer renderer = spriteAttachment.gameObject.GetComponent<SpriteRenderer> ();
			renderer.color = Color.white;
		}
	}
	
	// Target Detection collider is set as trigger. 
	// Physics layering prevents triggering for allied units.
	void OnTriggerEnter2D (Collider2D other)
	{
		if ((other.gameObject.tag == Tags.Units) && OnEnemyInRange != null) {
			OnEnemyInRange (other.gameObject);
		}
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.gameObject.tag == Tags.Units && OnEnemyExitRange != null) {
			OnEnemyExitRange (other.gameObject);
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Units") 
		{
			OnCollideWithEnemy (coll.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D coll)
	{
		if (coll.gameObject.tag == "Units") 
		{
			OnCollideWithEnemyExit (coll.gameObject);
		}
	}
}
