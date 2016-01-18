using UnityEngine;
using System;
using System.Collections;

public class FighterStateContext {

	private GameObject _fighter;

	#region Callbacks
	// Called when _cooldownState is set to ReadyState.
	public Action OnCooldownEnded;

	// Called when the attack animation finishes.
	public Action<Attack> OnAttackEnded;

	public Action OnDeath;
	#endregion

	private ActionState _actionState; 		// The current action. Walking, Attacking, etc.
	private GroundState _groundState;		// On the ground, in the air. Affects movement.
	private CooldownState _cooldownState;	// Tells if fighter is ready to act.
	
	// Constructor.
	public FighterStateContext (GameObject fighter)
	{
		_fighter = fighter;
		Initialize ();
	}
	
	// Update States here. Call this from your MonoBehaviour's Update method.ac
	public void Update ()
	{
		_actionState.Update ();
		_cooldownState.Update ();
	}

	private void Initialize ()
	{
		// Set initial states.
		_groundState   = new GroundState.OnGroundState (this);
		_actionState   = new ActionState.IdleState (this);
		_cooldownState = new CooldownState.ReadyState (this);
	}
	
	// Handle commands and state changes here.
	#region Public Methods

	public void Attack (Attack attackData, float cooldownDuration)
	{
		_actionState.Attack (attackData);
		_cooldownState.Cooldown (this, cooldownDuration);
	}

	public void Walk ()
	{
		_actionState.Walk ();
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

	public GroundState groundState {
		get {
			return _groundState;
		}	
		
		set {
			_groundState = value;
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



