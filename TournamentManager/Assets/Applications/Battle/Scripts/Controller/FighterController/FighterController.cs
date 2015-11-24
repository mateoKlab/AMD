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

		// comment out for now. (crashing)
//		SetSprite();
	}

	void OnDestroy () {
		(model as FighterModel).OnFighterDataSet -= OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy -= OnCollideWithEnemy;
	}

    public void SetSprite()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = ((FighterModel) model).fighterData.normalIcon;
    }

    public Attack GetAttackData()
    {
        FighterData fighter = ((FighterModel) GetComponent<Model>()).fighterData;

		return new Attack(fighter.ATK, 1.0f, AttackType.Melee, gameObject);
    }

    // Temporary...?
    IEnumerator Attack (GameObject enemy)
    {
		(view as FighterView).AnimateAttack ();

        yield return new WaitForSeconds(0.45f); // Temporary, use actual animation timing.

		((BattleController)app.controller).OnUnitAttack (gameObject, enemy);

		(view as FighterView).AnimateRun ();
	}

    public void OnReceiveAttack(Attack attack)
    {
        // TODO: Calculate skill effects, evade, block, etc.

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
//		((BattleController)app.controller).OnUnitAttack (gameObject, enemy);
		StartCoroutine ("Attack", enemy);
	}

	private void OnFighterDataSet ()
	{
		(view as FighterView).SetSprite ();
	}

	private void ReceiveDamage (Attack attack)
	{
		// Temporary. TODO: Apply armor damage reduction effects.
		(model as FighterModel).fighterData.HP -= attack.damage;

		Messenger.Send (EventTags.FIGHTER_RECEIVED_DAMAGE, attack.damage, this.gameObject);

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
			rigidBody.AddForce (new Vector2 (8.0f * -moveDirection, 1.0f), ForceMode2D.Impulse);
		}
	}
}
