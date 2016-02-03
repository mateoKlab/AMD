using UnityEngine;
using System.Collections;

public abstract class EngagementState {

	private FighterStateContext stateContext;
	
	public abstract void Engage ();
	public abstract void Disengage ();

	public class EngagedState : EngagementState
	{
		private int engageCount = 0;

		public EngagedState (FighterStateContext stateContext)
		{
			this.stateContext = stateContext;
			engageCount = 1;
		}

		public override void Engage ()
		{
			engageCount += 1;
		}

		public override void Disengage ()
		{
			engageCount -= 1;
			if (engageCount <= 0) {
				stateContext.engagementState = new EngagementState.DisengagedState (stateContext);
			}
		}
	}

	public class DisengagedState : EngagementState
	{
		public DisengagedState (FighterStateContext stateContext)
		{
			this.stateContext = stateContext;
		}

		public override void Engage ()
		{
			stateContext.engagementState = new EngagementState.EngagedState (stateContext);
		}

		public override void Disengage () { }
	}
}
