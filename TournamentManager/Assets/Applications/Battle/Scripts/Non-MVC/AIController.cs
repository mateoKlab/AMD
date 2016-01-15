using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AIController : MonoBehaviour {

	public List<FighterStateContext> allies;
	public List<FighterStateContext> enemies;

	public void StartWalking (FighterAlliegiance side)
	{
		List<FighterStateContext> stateList;

		if (side == FighterAlliegiance.Ally) {
			stateList = allies;
		} else {
			stateList = enemies;
		}


	}

}
