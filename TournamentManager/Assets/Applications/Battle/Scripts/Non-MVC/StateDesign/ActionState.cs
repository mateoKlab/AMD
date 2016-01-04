using UnityEngine;
using System.Collections;

// Abstract state for Fighter actions. Walking, Attacking, etc.
public abstract class ActionState {

	private FighterStateContext stateContext;
	
	public abstract void Update (); // Check for animations/cooldowns here.
	
	public abstract void Walk ();
//	public abstract void Attack (Attack attack);
	public virtual void Attack (Attack attackData)
	{
		if (stateContext.cooldownState is CooldownState.ReadyState) {
			stateContext.actionState = new AttackState (stateContext, attackData);
		}
	}

	public abstract void Hit ();
	public abstract void Idle ();


	// Stored reference to animator.
	private Animator animator;

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

			// Store reference to Animator and Transform.
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();
			fighterTransform = stateContext.fighter.transform;

			// Set move direction depending on FighterAllegiance.
			FighterModel fighterModel = stateContext.fighter.GetComponent<FighterModel> ();
			moveDirection = (int)fighterModel.allegiance;

		}

		public override void Attack (Attack attackData)
		{
			base.Attack (attackData);
//			// Attack only if not in cooldown.
//			if (stateContext.cooldownState is CooldownState.ReadyState) {
//
//				// TODO: Apply Fighter attack speed.
//				float randomCooldown = UnityEngine.Random.Range (0.5f, 1.0f);
//
//				stateContext.actionState = new AttackState (stateContext, attack);
//				stateContext.cooldownState.Cooldown (stateContext, randomCooldown); // cooldown duration temporary. TODO: Add to database.
//
//			}
		}

		public override void Hit ()
		{
			stateContext.actionState = new HitState (stateContext);
		}


		public override void Walk () { } // Already Walking...

		public override void Update () 
		{
			fighterTransform.position = new Vector3 (fighterTransform.position.x + (0.05f * moveDirection),
			                                         fighterTransform.position.y,
			                                         fighterTransform.position.z); //(transform.position.x + 1.0f) * moveDirection);
		}

		public override void Idle ()
		{
			stateContext.actionState = new ActionState.IdleState (stateContext);
		}
	}
	#endregion

	#region AttackState
	// Has two sub-states. OnCooldownState & AttackReadyState.
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
			animator.SetBool ("Attack", true);
		}
		
		public override void Update ()
		{
			// TEST. Set attack playback speed.
			animator.SetFloat ("AttackSpeed", 2.0f);


			// IF: attack animation over, transition to walk/idle.
			if (animator.GetNextAnimatorStateInfo (0).IsName ("walk2") && animator.IsInTransition (0)) {
				if (stateContext.OnAttackEnded != null) {
					stateContext.OnAttackEnded (attackData);
				}

				// TODO: Add condition: IF: enemy in range, { set to idle, wait for attack. } ELSE: set to walk.
				Walk ();
//				Idle ();
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

	#region HitState
	public class HitState : ActionState
	{
		// Constructor.
		public HitState (FighterStateContext context)
		{
			stateContext = context;			
		}

		public override void Update ()
		{
			// Update.
		}
		
		public override void Walk ()
		{ 
			stateContext.actionState = new WalkState (stateContext);
		}
		
		public override void Attack (Attack attack)
		{
			stateContext.actionState = new AttackState (stateContext, attack);
		}
		
		public override void Hit ()
		{
			// Already Hit.
		}

		public override void Idle ()
		{

		}
	}

	#region IdleState
	public class IdleState : ActionState
	{
		public IdleState (FighterStateContext context)
		{
			stateContext = context;

			animator.SetTrigger ("Idle");
		}

		public override void Update ()
		{
			// Update.
		}
		
		public override void Walk ()
		{ 
			stateContext.actionState = new WalkState (stateContext);
		}
		
		public override void Attack (Attack attackData)
		{
			base.Attack (attackData);
//			// Attack only if not in cooldown.
//			if (stateContext.cooldownState is CooldownState.ReadyState) {
//				
//				// TODO: Apply Fighter attack speed.
//				float randomCooldown = UnityEngine.Random.Range (1.0f, 1.5f);
//				
//				stateContext.actionState = new AttackState (stateContext, attack);
//				stateContext.cooldownState.Cooldown (stateContext, randomCooldown); // cooldown duration temporary. TODO: Add to database.
//			}
		}
		
		public override void Hit ()
		{
		}
		
		public override void Idle ()
		{
			
		}

	}
	#endregion

	#endregion
	#endregion
}
