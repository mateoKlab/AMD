using UnityEngine;
using System.Collections;

public abstract class KnockbackState {

	private FighterStateContext stateContext;
	
	private Rigidbody2D rigidBody; 			// Store reference to Rigidbody.
	
	public abstract void Update (); 		// Check for animations/cooldowns here.
	
	public abstract void Knockback (FighterStateContext context, float duration);
	public abstract void Recover (FighterStateContext context);

	public class KnockedBackState : KnockbackState {

		private float duration = 0.0f;
		private int knockbackDirection = 1;


		public KnockedBackState (FighterStateContext context, float duration)
		{
			stateContext = context;
			
			this.duration = duration;

			knockbackDirection = -(int)(stateContext.fighter.GetComponent<FighterModel> ()).allegiance;
			rigidBody = stateContext.fighter.GetComponent<Rigidbody2D> ();
		}

		public override void Update ()
		{
			// Apply force.
			if (duration > 0.0f) {
			
				// temp. apply force.
				if (stateContext.fighter.GetComponent<FighterModel> ().allegiance == FighterAlliegiance.Ally) {
					Debug.Log ("KB: " + duration * 10f);
				}

				rigidBody.velocity = Vector2.zero;
				rigidBody.angularVelocity = 0.0f;

				rigidBody.AddForce (new Vector2(duration * 5f * knockbackDirection, 0.0f), ForceMode2D.Impulse);
				duration -= Time.deltaTime;
			} else {

				stateContext.OnKnockbackEnded ();
				Recover (stateContext);
			}

		}

		public override void Recover (FighterStateContext context) 
		{
			stateContext.knockbackState = new RecoveredState (stateContext);
		}

		public override void Knockback (FighterStateContext context, float duration) { }	// Already being knocked back. Do nothing.

	}

	public class RecoveredState : KnockbackState {

		public RecoveredState (FighterStateContext context)
		{
			stateContext = context;

			rigidBody = stateContext.fighter.GetComponent<Rigidbody2D> ();

			rigidBody.velocity = Vector2.zero;
			rigidBody.angularVelocity = 0.0f;
		}

		public override void Update ()
		{
			
		}

		public override void Knockback (FighterStateContext context, float duration)
		{
			stateContext.knockbackState = new KnockedBackState (context, duration);
		}

		public override void Recover (FighterStateContext context) { }		// Already Recovered. Do nothing.

	}
}
