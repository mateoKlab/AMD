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

	public string name 
	{
		get 
		{
			return fighterData.name;
		}
	}
	
	public int healthPoints 
	{
		get
		{
			return fighterData.HP;
		}
	}
	
	public int attack
	{
		get
		{
			return fighterData.ATK;
		}
	}
	
	public int activeTroopIndex
	{
		get
		{
			return fighterData.activeTroopIndex;
		}
		set
		{
			fighterData.activeTroopIndex = value;
		}
	}
}
