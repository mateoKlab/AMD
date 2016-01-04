using UnityEngine;
using System.Collections;

// On Ground, in air.
public abstract class GroundState {
	
	protected FighterStateContext stateContext;
	
	//	public GroundState (FighterStateContext fighter)
	//	{
	//		// Set fighter context.
	//	}
	
	public abstract void Update ();
	
	public class OnGroundState : GroundState
	{

		public OnGroundState (FighterStateContext context)
		{
			stateContext = context;
		}

		public override void Update ()
		{
			// Check if onground, then change state.
		}
	}
	
	//	public class InAirState : GroundState
	//	{
	//
	//	}
	
	
}