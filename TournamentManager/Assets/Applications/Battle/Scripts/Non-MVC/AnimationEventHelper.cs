using UnityEngine;
using System;
using System.Collections;

public class AnimationEventHelper : MonoBehaviour {
	
	private FighterController parentController;
	private Attack attackData;

	// Use this for initialization
	void Start () {
		parentController = transform.parent.GetComponent<FighterController> ();
	}

	public void AttackStart (Attack attackData)
	{
		this.attackData = attackData;
	}

	void Attack ()
	{
		parentController.OnAttack (attackData);
	}
}
