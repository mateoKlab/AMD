using UnityEngine;
using System.Collections;
using Bingo;

public class MeleeFighterController : FighterController
{
	void FixedUpdate() 
	{
		// TEMPORARY.
		if (!((FighterModel)model).onGround) {
			return;
		}
		
		int moveDirection = (int)((FighterModel)GetComponent<Model> ()).allegiance;
		transform.position = new Vector3 (transform.position.x + (0.05f * moveDirection), transform.position.y, transform.position.x + (0.05f * moveDirection));
	}

	public void OnGroundEnter ()
	{
		((FighterModel)model).onGround = true;
	}
	
	public void OnGroundExit ()
	{
		((FighterModel)model).onGround = false;
	}
}
