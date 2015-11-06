using UnityEngine;
using System.Collections;
using Bingo;

public class FighterView : View {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		// TEST. TODO: use Tags.
		if (coll.gameObject.name == "Ground") 
		{
			// Enable Movement.
			((FighterController)controller).OnGroundEnter();
		}

		if (coll.gameObject.tag == Tags.Units) {
			((FighterController)controller).OnCollideWithEnemy(coll);
		}

	}

	void OnCollisionExit2D (Collision2D coll)
	{
		// TEST. TODO: use Tags.
		if (coll.gameObject.name == "Ground") 
		{
			// Disable Movement.
			((FighterController)controller).OnGroundExit();
		}
		
	}
}
