using UnityEngine;
using System.Collections;

public enum AttackType
{
	Melee,
	Ranged
}

public class Attack {

	public int damage;
	public float knockback;
	public AttackType type;

	public GameObject attackOrigin;

	//TODO: Effects List. e.g. Slow, Freeze, Stun, etc.

	public Attack (int damageAmount, float knockbackAmount, AttackType attackType, GameObject attacker)
	{
		damage = damageAmount;
		knockback = knockbackAmount;
		type = attackType;
		attackOrigin = attacker;
	}
}
