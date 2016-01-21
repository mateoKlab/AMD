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


	public FighterAlliegiance allegiance;
	
	#region FighterData
	public Action OnFighterDataSet;

	private FighterData _fighterData;
	public FighterData fighterData {
		get { 
			return _fighterData; 
		}
		set {

			_fighterData = value;

			if (OnFighterDataSet != null) {
				OnFighterDataSet ();
			}
		}
	}
	#endregion

	#region Enemy Detection
	private List<GameObject> enemiesInRange = new List<GameObject> ();
	 
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
	#endregion

}
