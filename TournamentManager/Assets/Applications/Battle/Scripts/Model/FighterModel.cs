using UnityEngine;
using System.Collections;
using Bingo;

public class FighterModel : Model {

	public FighterData fighterData;
	public bool onGround;


	public FighterAlliegiance allegiance;

	public enum FighterAlliegiance {
		Ally = 1,
		Enemy = -1
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
