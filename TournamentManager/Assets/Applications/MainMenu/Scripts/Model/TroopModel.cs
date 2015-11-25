using UnityEngine;
using System.Collections;
using Bingo;

public class TroopModel : Model
{
	public FighterData fighterData;
	
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
}
