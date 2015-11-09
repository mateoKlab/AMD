using UnityEngine;
using System.Collections;
using Bingo;

public class TroopModel : Model
{
	private FighterData fighterData;

	public void SetFighterData(FighterData fighterData)
	{
		this.fighterData = fighterData;
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
}
