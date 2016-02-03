using UnityEngine;
using System;
using System.Collections;

public class FighterStateContext {

	private GameObject _fighter;

	#region Callbacks
	// Called when _cooldownState is set to ReadyState.
	public Action OnCooldownEnded;

	// Called when the attack animation finishes.
	public Action OnAttackEnded;

	// Called at the end of a knockback.
	public Action OnKnockbackEnded;

	public Action OnDeath;
	#endregion

	private ActionState _actionState; 		  // The current action. Walking, Attacking, etc.
	private KnockbackState _knockbackState;   // Flinched/Stunned state.
	private EngagementState _engagementState; // Colliding with an enemy or not.
	private CooldownState _cooldownState;	  // Tells if fighter is ready to act.
	
	// Constructor.
	public FighterStateContext (GameObject fighter)
	{
		_fighter = fighter;
		Initialize ();
	}
	
	// Update States here. Call this from your MonoBehaviour's Update method.
	public void Update ()
	{
		_actionState.Update ();
		_cooldownState.Update ();
		_knockbackState.Update ();
	}

	private void Initialize ()
	{
		// Set initial states.
		_actionState     = new ActionState.WalkState (this);
		_knockbackState  = new KnockbackState.RecoveredState (this);
		_cooldownState   = new CooldownState.ReadyState (this);
		_engagementState = new EngagementState.DisengagedState (this);
	}
	
	// Handle commands and state changes here.
	#region Public Methods

	public void Attack ()
	{
		if (cooldownState is CooldownState.ReadyState) {
			_actionState = new ActionState.AttackState (this);
		}
	}

	public void AttackEnded ()
	{
		OnAttackEnded ();
	}

	public void BlockEnded ()
	{

	}

	public void CollisionEnter ()
	{
		engagementState.Engage ();
	}

	public void CollisionExit ()
	{
		engagementState.Disengage ();
	}

	public void Walk ()
	{
		_actionState.Walk ();
	}

	public void Knockback (float knockbackDuration)
	{
		if (!(_actionState is ActionState.AttackState)) {
			_actionState = new ActionState.BlockState (this);
		}

		_knockbackState.Knockback (this, knockbackDuration);
	}

	public void StartCooldown (float cooldownDuration)
	{
		_cooldownState.Cooldown (this, cooldownDuration);
	}

	#endregion


	#region Getters/Setters
	public GameObject fighter {
		get {
			return _fighter;
		}

		set {
			_fighter = value;
			// Do stuff.
		}
	}

	public ActionState actionState {
		get {
			return _actionState;
		}

		set {
			_actionState = value;
		}
	}

	public KnockbackState knockbackState {
		get {
			return _knockbackState;
		}
		
		set {
			_knockbackState = value;
		}
	}

	public EngagementState engagementState {
		get {
			return _engagementState;
		}
		
		set {
			_engagementState = value;
		}
	}

	public CooldownState cooldownState {
		get {
			return _cooldownState;
		}	
		
		set {
			_cooldownState = value;

			if (_cooldownState is CooldownState.ReadyState) {
				OnCooldownEnded ();
			}
		}
	}
	
	#endregion
}



