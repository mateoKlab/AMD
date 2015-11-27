using UnityEngine;
using System;
using System.Collections;
using Bingo;

public class FighterController : Controller
{
	// Use this for initialization
	public virtual void Awake () {
		(model as FighterModel).OnFighterDataSet += OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy += OnCollideWithEnemy;

		// comment out for now. (crashing due to changes.)
//		SetSprite();
	}

	void OnDestroy () {
		(model as FighterModel).OnFighterDataSet -= OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy -= OnCollideWithEnemy;
	}

    public void SetFighterSkin ()
    {
		(view as FighterView).SetFighterSkin ((model as FighterModel).fighterData.skinData);

//        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
//        sr.sprite = ((FighterModel) model).fighterData.normalIcon;
    }

    public Attack GetAttackData()
    {
        FighterData fighter = ((FighterModel) GetComponent<Model>()).fighterData;

		return new Attack(fighter.ATK, 1.0f, AttackType.Melee, gameObject);
    }

    public void OnReceiveAttack(Attack attack)
    {
        // TODO: Calculate skill effects, evade, block, etc.

		// TEST.
		(view as FighterView).AnimateHit ();

        ReceiveDamage(attack);
        ReceiveKnockback(attack.knockback);

        // HACK.
//		OnGroundExit ();
    }

	public void OnGroundEnter ()
	{
		((FighterModel)model).onGround = true;
	}

	public void OnGroundExit ()
	{
		((FighterModel)model).onGround = false;
	}

	private void OnCollideWithEnemy (GameObject enemy)
	{
		((BattleController)app.controller).OnUnitAttack (gameObject, enemy);
	}

	private void OnFighterDataSet ()
	{
		SetFighterSkin ();
	}

	private void ReceiveDamage (Attack attack)
	{
		// Temporary. TODO: Apply armor damage reduction effects.
		(model as FighterModel).fighterData.HP -= attack.damage;

		Messenger.Send (EventTags.FIGHTER_RECEIVED_DAMAGE, attack.damage, this.gameObject);

//		(view as FighterView).fighterSprite.GetComponent<Animator> ().SetTrigger ("Hit");

		// TODO: Move to model. Use delegate.
		if ((model as FighterModel).fighterData.HP <= 0) {
			Messenger.Send (EventTags.FIGHTER_KILLED, this.gameObject, attack.attackOrigin);
		}
	}

	private void ReceiveKnockback (float knockback)
	{
		// Temporary konckback. TODO: Apply knockback resistance/amount.
		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();

		if (Math.Abs (rigidBody.velocity.x) < 1.0f) {
			rigidBody.AddForce (new Vector2 (9.0f * -moveDirection, 1.0f), ForceMode2D.Impulse);
		}
	}
}
