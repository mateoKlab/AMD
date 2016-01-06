using UnityEngine;
using System;
using System.Collections;

public abstract class CooldownState {

	private FighterStateContext stateContext;

	public abstract void Update ();
	public abstract void Cooldown (FighterStateContext context, float cooldownDuration);
	
	#region Concrete States
	public class OnCooldownState : CooldownState
	{
		private float cooldownLeft;

		// Constructor.
		public OnCooldownState (FighterStateContext context, float cooldownDuration)
		{
			stateContext = context;

			cooldownLeft = cooldownDuration;
		}

		public override void Update ()
		{
			cooldownLeft -= Time.deltaTime;

			if (cooldownLeft <= 0) {
				stateContext.cooldownState = new ReadyState (stateContext);
			}
		}

		public override void Cooldown (FighterStateContext context, float cooldownDuration)
		{
			// Already On cooldown. Reset timer.
			cooldownLeft = cooldownDuration;
			stateContext = context;
		}
	}

	public class ReadyState : CooldownState
	{
		// Constructor.
		public ReadyState (FighterStateContext context)
		{
			stateContext = context;
		}

		public override void Update ()
		{
			// Not on cooldown.
		}
		
		public override void Cooldown (FighterStateContext context, float cooldownDuration)
		{
			stateContext.cooldownState = new OnCooldownState (context, cooldownDuration);
		}
	}
	#endregion
}
