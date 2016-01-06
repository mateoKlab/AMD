using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public enum FighterAlliegiance {
	Ally = 1,
	Enemy = -1
}

public class FighterModel : Model {

	public Action OnFighterDataSet;

	// TODO: Deprecate.
	public bool onGround;
	// TODO: Deprecate.
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

	private List<GameObject> enemiesInRange = new List<GameObject> ();

	public FighterAlliegiance allegiance;
	 
	public GameObject GetEnemyInRange ()
	{
		if (enemiesInRange.Count > 0) {

			// Remove enemy from list if already dead.
			if (enemiesInRange [0] == null) {
				enemiesInRange.RemoveAt (0);
				return GetEnemyInRange ();
			}

			return enemiesInRange[0];
		}

		return null;
	}

	public bool IsEnemyInRange (GameObject enemy)
	{
		return enemiesInRange.Contains (enemy);
	}

	public void AddEnemyInRange (GameObject enemy)
	{
		if (!enemiesInRange.Contains (enemy)) {
			enemiesInRange.Add (enemy);
		}
	}

	public void RemoveEnemyInRange (GameObject enemy)
	{
		if (enemiesInRange.Contains (enemy)) {
			enemiesInRange.Remove (enemy);
		}
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

	public int cost
	{
		get
		{
			return fighterData.cost;
		}
	}

    public FighterSkinData skindData
    {
        get
        {
            return fighterData.skinData;
        }
    }
}
