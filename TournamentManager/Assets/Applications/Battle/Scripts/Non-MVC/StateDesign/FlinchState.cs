using UnityEngine;
using System.Collections;

public abstract class FlinchState {

	private FighterStateContext stateContext;
	
	private Animator animator; 					// Stored reference to animator.
	
//	public abstract void Update (); 			// Check for animations/cooldowns here.
	
//	public abstract void Flinch (FighterStateContext context);
//	public abstract void Recover (FighterStateContext context);


	public class FlinchingState : FlinchState
	{

		public FlinchingState (FighterStateContext context, float duration)
		{

		}

	}

	public class RecoveredState : FlinchState
	{

	}
}


