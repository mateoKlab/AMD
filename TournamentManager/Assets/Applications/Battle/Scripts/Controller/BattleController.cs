using UnityEngine;
using System.Collections;
using Bingo;

public class BattleController : Controller<Battle>
{
	public GameObject FighterPrefab;


	// HACK ..TEST CODE.. HACK
	public void SpawnFighters ()
	{
		Vector3 startPos = new Vector3 (-5f, -1f, 0f);

		foreach (FighterData fighter in GameData.Instance.PlayerData.fightersOwned) {

			GameObject newFighter = Instantiate (FighterPrefab);
			newFighter.SetActive (true);


			newFighter.GetComponent <FighterModel> ().FighterData = fighter;
			newFighter.GetComponent <FighterModel> ().allegiance = FighterModel.FighterAlliegiance.Ally;

			newFighter.transform.position = startPos;

			startPos = new Vector3 (-5f - 1f, 1f, 0f);
		}

		StageData testData = new StageData ();

		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 2000;
		testFighter1.ATK = 50;

//		FighterData testFighter2 = new FighterData ();
//		testFighter2.HP = 2000;
//		testFighter2.ATK = 50;

		testData.enemies.Add (testFighter1);
//		testData.enemies.Add (testFighter2);

		startPos = new Vector3 (5f, 1f, 0f);

		foreach (FighterData fighter in testData.enemies) {
			GameObject newFighter = Instantiate (FighterPrefab);
			newFighter.layer = LayerMask.NameToLayer("EnemyUnits");
			newFighter.SetActive (true);

			
			newFighter.GetComponent <FighterModel> ().FighterData = fighter;
			newFighter.GetComponent <FighterModel> ().allegiance = FighterModel.FighterAlliegiance.Enemy;

			newFighter.transform.position = startPos;
			newFighter.transform.rotation = Quaternion.Euler(0,180f,0);

			startPos = new Vector3 (5f + 1f, 1f, 0f);
		}
	}

	public void OnUnitAttack (GameObject attacker, GameObject defender)
	{
		FighterController attackingUnit = ((FighterController)attacker.GetComponent<FighterController> ());
		FighterController defendingUnit = ((FighterController)defender.GetComponent<FighterController> ());

		defendingUnit.OnReceiveAttack (attackingUnit.Attack ());
	}

}

