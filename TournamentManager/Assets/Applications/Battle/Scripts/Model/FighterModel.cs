using UnityEngine;
using System;
using System.Collections;
using Bingo;

public enum FighterAlliegiance {
	Ally = 1,
	Enemy = -1
}

public class FighterModel : Model {

	public Action OnFighterDataSet;

	public bool onGround;

	public bool isRanged;


	private FighterData _fighterData;

	public FighterData fighterData {
		get { return _fighterData; }
		set {

			_fighterData = value;

			isRanged = _fighterData.isRanged;
			if (OnFighterDataSet != null) {
				OnFighterDataSet ();
			}
		}
	}

	public FighterAlliegiance allegiance;

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

	public int cost
	{
		get
		{
			return fighterData.cost;
		}
	}
}
