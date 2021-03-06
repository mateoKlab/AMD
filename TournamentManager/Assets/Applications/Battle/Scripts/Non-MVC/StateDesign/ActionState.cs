﻿using UnityEngine;
using System.Collections;

// Abstract state for Fighter actions. Walking, Attacking, etc.
public abstract class ActionState {

	private FighterStateContext stateContext;

	private Animator animator; 					// Stored reference to animator.

	public abstract void Update (); 			// Check for animations/cooldowns here.
	
	public abstract void Walk ();
	public abstract void Attack (Attack attackData);
	public abstract void Hit ();
	public abstract void Idle ();


	#region Concrete States

	#region WalkState
	public class WalkState : ActionState
	{
		private Transform fighterTransform;
		private int moveDirection = 1;

		// Constructor.
		public WalkState (FighterStateContext context)
		{
			stateContext = context;
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();
			fighterTransform = stateContext.fighter.transform;

			// Set move direction depending on FighterAllegiance.
			FighterModel fighterModel = stateContext.fighter.GetComponent<FighterModel> ();
			moveDirection = (int)fighterModel.allegiance;

			animator.SetTrigger ("Walk");
		}

		// Update position while in this state.
		public override void Update () 
		{
			fighterTransform.position = new Vector3 (fighterTransform.position.x + (0.05f * moveDirection),
			                                         fighterTransform.position.y,
			                                         fighterTransform.position.z); // (transform.position.x + 1.0f) * moveDirection);
		}

		public override void Attack (Attack attackData)
		{
			if (stateContext.cooldownState is CooldownState.ReadyState) {
				stateContext.actionState = new AttackState (stateContext, attackData);
			}
		}

		public override void Idle ()
		{
			stateContext.actionState = new ActionState.IdleState (stateContext);
		}

		public override void Hit () { }

		public override void Walk () { } // Already Walking. Do nothing.
	}
	#endregion

	#region AttackState
	public class AttackState : ActionState
	{
		public Attack attackData;

		// Constructor.
		public AttackState (FighterStateContext context, Attack attack)
		{
			stateContext = context;
			attackData = attack;

			// Store reference to animator.
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();

			// Start attacking.
			animator.SetTrigger ("Attack");
		}
		
		public override void Update ()
		{
			// TEST: attack animation names, don't match. Remove once fixed.
			if ((animator.GetCurrentAnimatorStateInfo (0).IsName ("attack") || animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack")) && animator.IsInTransition (0)) { //&& !animator.IsInTransition (0)) {

				if (stateContext.OnAttackEnded != null) {
					stateContext.OnAttackEnded (attackData);
				}

				Idle ();
			}
		}
		
		public override void Walk ()
		{ 
			stateContext.actionState = new WalkState (stateContext);
		} 

		public override void Hit ()
		{
			stateContext.actionState = new HitState (stateContext);
		}

		public override void Attack (Attack attack) { } // Already Attacking. Do nothing.

		public override void Idle ()
		{
			stateContext.actionState = new ActionState.IdleState (stateContext);
		}

	}
	#endregion
	
	#region IdleState
	public class IdleState : ActionState
	{
		public IdleState (FighterStateContext context)
		{
			stateContext = context;
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();

			animator.SetTrigger ("Idle");
		}

		public override void Attack (Attack attackData)
		{
			if (stateContext.cooldownState is CooldownState.ReadyState) {
				stateContext.actionState = new AttackState (stateContext, attackData);
			}
		}

		public override void Update ()
		{
			// Update.
		}
		
		public override void Walk ()
		{ 
			stateContext.actionState = new WalkState (stateContext);
		}

		public override void Hit () { }
		
		public override void Idle () { } // Already Idle. Do nothing.
	}
	#endregion

	#region HitState
	public class HitState : ActionState
	{
		// Constructor.
		public HitState (FighterStateContext context)
		{
			stateContext = context;			
		}
		
		public override void Update () { }
		
		public override void Walk () { }
		
		public override void Attack (Attack attack) { }
		
		public override void Hit () { }
		
		public override void Idle () { }
	}
	#endregion

	#endregion
}
