using UnityEngine;
using System.Collections;
using Bingo;

public class BattleController : Controller<Battle>
{
	public GameObject FighterPrefab;

	public void SpawnFighters ()
	{
		foreach (FighterData fighter in app.model.PlayerData.fightersOwned) {
			// TODO: Instantiate Fighter MVC Prefab.

			GameObject newFighter = Instantiate (FighterPrefab);

		}

		foreach (FighterData fighter in app.model.StageData.enemies) {

		}
	}

	public void OnUnitAttack (GameObject attacker, GameObject defender)
	{
		FighterController attackingUnit = ((FighterController)attacker.GetComponent<FighterController> ());
		FighterController defendingUnit = ((FighterController)defender.GetComponent<FighterController> ());

		defendingUnit.OnReceiveAttack (attackingUnit.Attack ());
	}

}

