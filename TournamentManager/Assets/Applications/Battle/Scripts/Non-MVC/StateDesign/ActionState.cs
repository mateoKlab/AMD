using UnityEngine;
using System.Collections;

// Abstract state for Fighter actions. Walking, Attacking, etc.
public abstract class ActionState {

	private FighterStateContext stateContext;

	private Animator animator; 					// Stored reference to animator.

	public abstract void Update (); 			// Check for animations/cooldowns here.
	
	public abstract void Walk ();
	public abstract void Attack ();
	public abstract void Hit ();
	public abstract void Idle ();
	public abstract void Death ();


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
			                                         fighterTransform.position.z);

		}

		public override void Attack ()
		{
			if (stateContext.cooldownState is CooldownState.ReadyState) {
				stateContext.actionState = new AttackState (stateContext);
			}
		}

		public override void Idle ()
		{
			stateContext.actionState = new ActionState.IdleState (stateContext);
		}

		public override void Hit () { }

		public override void Walk () { } // Already Walking. Do nothing.

		public override void Death ()
		{
			stateContext.actionState = new DeathState (stateContext);
		}
	}
	#endregion

	#region AttackState
	public class AttackState : ActionState
	{
		// Constructor.
		public AttackState (FighterStateContext context)
		{
			stateContext = context;

			// Store reference to animator.
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();

			// SetTrigger for attack animation.
			string attackString = "Attack";
			int randomAttack = UnityEngine.Random.Range (1, 4);

			animator.SetTrigger (attackString + randomAttack.ToString ());
		}
		
		public override void Update ()
		{
			// Set state to Idle when attack animation is finished.
//			if ((animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack1")
//			   || animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack2") 
//			    || animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack3")) && animator.IsInTransition (0)) {
//
//					stateContext.AttackEnded ();
//
//				Idle ();
//			}

			AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);	
			if( (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack3"))/* && animator.IsInTransition (0)*/) {
				float playbackTime = currentState.normalizedTime % 1;
				if(playbackTime>= .9f) {
					stateContext.AttackEnded ();
					Idle ();
				}
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

		public override void Attack () { } // Already Attacking. Do nothing.

		public override void Idle ()
		{
			stateContext.actionState = new ActionState.IdleState (stateContext);
		}

		public override void Death ()
		{
			stateContext.actionState = new DeathState (stateContext);
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

		public override void Attack ()
		{
			if (stateContext.cooldownState is CooldownState.ReadyState) {
				stateContext.actionState = new AttackState (stateContext);
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
		
		public override void Idle () {} // Already Idle. Do nothing.

		public override void Death ()
		{
			stateContext.actionState = new DeathState (stateContext);
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
		
		public override void Update () { }
		
		public override void Walk () { }
		
		public override void Attack () { }
		
		public override void Hit () { }
		
		public override void Idle () { }

		public override void Death ()
		{
			stateContext.actionState = new DeathState (stateContext);
		}
	}
	#endregion

	#region DeathState
	public class DeathState : ActionState
	{
		private float deathTimer = 2.5f;

		// Constuctor.
		public DeathState (FighterStateContext context)
		{
			stateContext = context;
			animator = stateContext.fighter.GetComponentInChildren<Animator> ();

			animator.SetTrigger ("Death");
		}

		public override void Update ()
		{
			deathTimer -= Time.deltaTime;

			if (deathTimer < 0) {
				if (stateContext.OnDeath != null) {
					stateContext.OnDeath ();
				}
			}
		}
		
		public override void Walk () { }
		
		public override void Attack () { }
		
		public override void Hit () { }
		
		public override void Idle () { }

		public override void Death () { }

	}
	#endregion

	#endregion
}
