using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class FighterController : Controller
{
	public FighterStateContext state;

	// Use this for initialization
	public virtual void Start () {
		state = new FighterStateContext (this.gameObject);
		state.OnCooldownEnded += OnCooldownEnded;
		state.OnAttackEnded   += OnAttack;

		(model as FighterModel).OnFighterDataSet += OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy += OnCollideWithEnemy;
		(view as FighterView).OnEnemyInRange	 += OnEnemyInRange;
		(view as FighterView).OnEnemyExitRange 	 += OnEnemyExitRange;
	}

	void OnDestroy () {
		state.OnCooldownEnded -= OnCooldownEnded;
		state.OnAttackEnded   -= OnAttack;

		(model as FighterModel).OnFighterDataSet -= OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy -= OnCollideWithEnemy;
		(view as FighterView).OnEnemyInRange	 -= OnEnemyInRange;
		(view as FighterView).OnEnemyExitRange 	 -= OnEnemyExitRange;
	}

	protected void FixedUpdate ()
	{
		state.Update ();
	}

	#region TEMPORARY
	public void OnGroundEnter ()
	{
		((FighterModel)model).onGround = true;
	}
	
	public void OnGroundExit ()
	{
		((FighterModel)model).onGround = false;
	}
	#endregion

    public void OnReceiveAttack(Attack attack)
    {
        // TODO: Calculate skill effects, evade, block, etc.

        ReceiveDamage(attack);
//        ReceiveKnockback(attack.knockback);
    }

	#region View Delegates

	private void OnEnemyInRange (GameObject enemy)
	{
		(model as FighterModel).AddEnemyInRange (enemy);
		Attack (); // Trigger attack state if not in cooldown.
	}
	
	private void OnEnemyExitRange (GameObject enemy)
	{
		(model as FighterModel).RemoveEnemyInRange (enemy);
	}

	
	private void OnCollideWithEnemy (GameObject enemy)
	{
		
	}

	#endregion
	#region Model Delegates

	private void OnFighterDataSet ()
	{
		SetFighterSkin ();
	}

	#endregion
	#region StateDesign Callbacks
	
	// Callback for end of cooldown.
	void OnCooldownEnded ()
	{
		if ((model as FighterModel).GetEnemyInRange () != null) {
			Attack ();
		} else {
			Walk ();
		}
	}
	
	// Callback for end of attack animation.
	public virtual void OnAttack (Attack attack)
	{
		// OVERRIDE ME.
	}
	
	#endregion

	#region Private Methods

	private void Attack ()
	{
		GameObject enemyInRange = (model as FighterModel).GetEnemyInRange ();
		if (enemyInRange != null) {

			// TODO: Use attackspeed for cooldown.
			float tempCooldown = UnityEngine.Random.Range (0.75f, 1.25f);
			state.Attack (GetAttackData (enemyInRange), tempCooldown);
		}
	}

	private void Walk ()
	{
		state.Walk ();
	}

	private void ReceiveDamage (Attack attack)
	{
		// TODO: Apply armor/damage reduction effects.
		(model as FighterModel).fighterData.HP -= attack.damage;

		Messenger.Send (EventTags.FIGHTER_RECEIVED_DAMAGE, attack.damage, this.gameObject);

		// TODO: Move to model. Use delegate.
		if ((model as FighterModel).fighterData.HP <= 0) {
			Messenger.Send (EventTags.FIGHTER_KILLED, this.gameObject, attack.attackOrigin);
		}

		(view as FighterView).SetSpriteColor ();
		// Edit: AJ (Test)
		DamageManager.instance.ActivateDamageElement(transform.position, attack.damage, (model as FighterModel).allegiance == FighterAlliegiance.Ally);
		SoundManager.instance.PlayHitSFX();
		if (UnityEngine.Random.value < 0.1f) 
		{
			SoundManager.instance.PlayGruntSFX();
		}
	}

	private void ReceiveKnockback (float knockback)
	{
		// TODO: Apply knockback resistance/amount.
		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;

		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();

		if (Math.Abs (rigidBody.velocity.x) < 1.0f) {
			rigidBody.AddForce (new Vector2 (9.0f * -moveDirection, 1.0f), ForceMode2D.Impulse);
		}
	}

	private Attack GetAttackData (GameObject attackTarget)
	{
		FighterData fighter = ((FighterModel) GetComponent<Model>()).fighterData;
		
		return new Attack(fighter.ATK, 1.0f, AttackType.Melee, gameObject, attackTarget);
	}

	private void SetFighterSkin ()
	{
		(view as FighterView).SetFighterSkin ((model as FighterModel).fighterData.skinData);
	}

	#endregion
}
