﻿using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class ProjectileController : Controller
{
	public Action<GameObject> OnDestroyProjectile;

	private int moveDirection;

	// temp.
	private float projectileSpeed = 0.15f;

	void Awake ()
	{
		(model as ProjectileModel).OnAttackDataSet += OnAttackDataSet;
		(view as ProjectileView).OnHitEnemy += OnHitEnemy;
	}

	void OnDestroy () {
		(model as ProjectileModel).OnAttackDataSet -= OnAttackDataSet;
		(view as ProjectileView).OnHitEnemy -= OnHitEnemy;
	}

	void FixedUpdate ()
	{
		transform.position = new Vector3 (transform.position.x + (projectileSpeed * moveDirection) , transform.position.y, transform.position.z);
	}

	void OnAttackDataSet (Attack attackData)
	{
		FighterModel fighterModel = (model as ProjectileModel).attackData.attackOrigin.GetComponent<FighterModel> ();
		moveDirection = (int)fighterModel.allegiance;

		if (fighterModel.allegiance == FighterAlliegiance.Ally) {
			gameObject.layer = LayerMask.NameToLayer("AlliedProjectile");
		} else {
			gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
		}
	}

	public void OnHitEnemy (GameObject enemy)
	{
		((BattleController)app.controller).OnProjectileHit ((model as ProjectileModel).attackData, enemy);

		if (OnDestroyProjectile != null) {
			OnDestroyProjectile (gameObject);
		}
	}
}
