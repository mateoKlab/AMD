using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class ProjectileController : Controller
{
	public Action<GameObject> OnDestroyProjectile;

	private int moveDirection;

	void Awake ()
	{
		(model as ProjectileModel).OnFighterSet += OnFighterSet;
		(view as ProjectileView).OnHitEnemy += OnHitEnemy;
	}

	void OnDestroy () {
		(model as ProjectileModel).OnFighterSet -= OnFighterSet;
		(view as ProjectileView).OnHitEnemy -= OnHitEnemy;
	}

	void FixedUpdate ()
	{
		transform.position = new Vector3 (transform.position.x + (0.1f * moveDirection) , transform.position.y, transform.position.z);
	}

	void OnFighterSet (GameObject fighter)
	{
		FighterModel fighterModel = (model as ProjectileModel).fighter.GetComponent<FighterModel> ();
		moveDirection = (int)fighterModel.allegiance;

		if (fighterModel.allegiance == FighterAlliegiance.Ally) {
			gameObject.layer = LayerMask.NameToLayer("AlliedProjectile");
		} else {
			gameObject.layer = LayerMask.NameToLayer("EnemyProjectile");
		}
	}

	public void OnHitEnemy (GameObject enemy)
	{
		((BattleController)app.controller).OnUnitAttack ((model as ProjectileModel).fighter, enemy);

		if (OnDestroyProjectile != null) {
			OnDestroyProjectile (gameObject);
		}
	}
}
