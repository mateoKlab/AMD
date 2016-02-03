using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class FighterController : Controller
{
	public FighterStateContext state;

	// Receives animation events and sends callbacks.
	private AnimationEventHelper eventHelper;

	// Use this for initialization
	public virtual void Start () {
		state = new FighterStateContext (this.gameObject);
		state.OnCooldownEnded  += OnCooldownEnded;
		state.OnAttackEnded    += OnAttackEnded;
		state.OnKnockbackEnded += OnKnockbackEnded;
		state.OnDeath 		   += OnDeath;

		(view as FighterView).OnCollideWithEnemyExit += OnCollideWithEnemyExit;
		(model as FighterModel).OnFighterDataSet	 += OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy	 += OnCollideWithEnemy;
		(view as FighterView).OnEnemyInRange		 += OnEnemyInRange;
		(view as FighterView).OnEnemyExitRange 		 += OnEnemyExitRange;

		eventHelper = GetComponentInChildren<AnimationEventHelper> ();
	}

	void OnDestroy () {
		state.OnCooldownEnded  -= OnCooldownEnded;
		state.OnAttackEnded    -= OnAttackEnded;
		state.OnKnockbackEnded -= OnKnockbackEnded;
		state.OnDeath 		   -= OnDeath;

		(view as FighterView).OnCollideWithEnemyExit -= OnCollideWithEnemyExit;
		(model as FighterModel).OnFighterDataSet	 -= OnFighterDataSet;
		(view as FighterView).OnCollideWithEnemy	 -= OnCollideWithEnemy;
		(view as FighterView).OnEnemyInRange		 -= OnEnemyInRange;
		(view as FighterView).OnEnemyExitRange 		 -= OnEnemyExitRange;

	}

	protected void FixedUpdate ()
	{
		state.Update ();
	}

	#region TEMPORARY
	public void OnGroundEnter ()
	{
//		state.groundState
//		((FighterModel)model).onGround = true;
	}
	
	public void OnGroundExit ()
	{
//		((FighterModel)model).onGround = false;
	}
	#endregion

	public void StartBattle ()
	{
		StartAttack ();
	}

    public void OnReceiveAttack(Attack attack)
    {
        // TODO: Calculate skill effects, evade, block, etc. here.

        ReceiveDamage (attack);
		ReceiveKnockback (attack);
    }

	public void RemoveFromRange (GameObject enemy)
	{
		(model as FighterModel).RemoveEnemyInRange (enemy);

		if ((model as FighterModel).GetEnemyInRange () != null) {
			Walk ();	
		}
	}

	public void CheckRange ()
	{
		// check for enemy in range.
	}

	#region View Delegates

	private void OnEnemyInRange (GameObject enemy)
	{
		(model as FighterModel).AddEnemyInRange (enemy);
		StartAttack (); // Trigger attack state if not in cooldown.
	}
	
	private void OnEnemyExitRange (GameObject enemy)
	{
		RemoveFromRange (enemy);
	}

	
	private void OnCollideWithEnemy (GameObject enemy)
	{

	}

	private void OnCollideWithEnemyExit (GameObject enemy)
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
		StartAttack ();
	}

	// Called at the point of attack during the attack animation.
	public virtual void OnAttack (Attack attackData)
	{
		// OVERRIDE ME.
	}

	// Called at the end of the attack animation.
	protected virtual void OnAttackEnded ()
	{
		// TODO: Use attackspeed for cooldown.
		float tempCooldown = UnityEngine.Random.Range (0.75f, 2.00f);

		state.StartCooldown (tempCooldown);
	}

	protected virtual void OnKnockbackEnded ()
	{
		StartAttack ();
	}

	// Callback for end of death animation/timer.
	void OnDeath ()
	{
		Messenger.Send (EventTags.FIGHTER_DEATH, this.gameObject);
	}
	
	#endregion

	#region Private Methods
	// Called at the start of an attack.
	protected virtual void StartAttack ()
	{	
		GameObject enemyInRange = (model as FighterModel).GetEnemyInRange ();
		if (enemyInRange != null) {

			Attack attack = GetAttackData (enemyInRange);

			state.Attack ();
			eventHelper.AttackStart (attack);
		
		} else {
			Walk ();
		}
	}

	protected void Walk ()
	{
		state.Walk ();
	}

	protected void ReceiveAttack (Attack attack)
	{

	}

	protected void ReceiveDamage (Attack attack)
	{
		// TODO: Apply armor/damage reduction effects.e
		(model as FighterModel).fighterData.HP -= 50; //attack.damage;

		Messenger.Send (EventTags.FIGHTER_RECEIVED_DAMAGE, attack.damage, this.gameObject);

		// TODO: Move to model. Use delegate.
		if ((model as FighterModel).fighterData.HP <= 0) {
			state.actionState.Death ();
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

	protected void ReceiveKnockback (Attack attack)
	{
		state.Knockback (0.75f);

//		// TODO: Apply knockback resistance/amount.
//		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;
//
//		Rigidbody2D rigidBody = GetComponent<Rigidbody2D> ();
//
//		if (Math.Abs (rigidBody.velocity.x) < 1.0f) {
//			rigidBody.AddForce (new Vector2 (1.0f * -moveDirection, 0.0f), ForceMode2D.Impulse);
//		}
	}

	protected Attack GetAttackData (GameObject attackTarget)
	{
		FighterData fighter = ((FighterModel) GetComponent<Model>()).fighterData;
		
		return new Attack(fighter.ATK, 1.0f, AttackType.Melee, gameObject, attackTarget);
	}

	protected void SetFighterSkin ()
	{
		(view as FighterView).SetFighterSkin ((model as FighterModel).fighterData.skinData);
	}

	#endregion
}
