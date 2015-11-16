using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class ProjectileView : View
{
	public Action<GameObject> OnHitEnemy;

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (coll.gameObject.tag == Tags.Units) {
			OnHitEnemy (coll.gameObject);
		}
	}

}
