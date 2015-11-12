using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class BattleController : Controller<Battle>
{
	public GameObject FighterPrefab;

	public List<FighterData> enemies = new List<FighterData> ();
	
	void Start ()
	{
		Messenger.AddListener (EventTags.FIGHTER_KILLED, FighterKilled);

	}

	void FighterKilled (params object[] args)
	{
		enemies.Remove ((FighterData)args [0]);

		if (enemies.Count == 0) {
			Debug.Log ("WIN");
			// TEST...

		
			GameData.instance.playerData.gold += 1000;

//			if(GameData.instance.playerData.tournamentProgress == GameData.instance.playerData.unlockedStages.Count - 1 
//			   && GameData.instance.currentStage.id == GameData.instance.playerData.unlockedStages[GameData.instance.playerData.unlockedStages.Count - 1]
//			   && GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount - 1)
//				GameData.instance.playerData.tournamentProgress++;
			if (GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount)
				GameData.instance.playerData.tournamentProgress++;
			Application.LoadLevel ("MainMenuScene");
		}

	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (EventTags.FIGHTER_KILLED, FighterKilled);
	}
		                     
	// HACK ..TEST CODE.. HACK
	public void SpawnFighters ()
	{
		Vector3 startPos = new Vector3 (-5f, -1f, 0f);

		foreach (FighterData fighter in GameData.instance.playerData.fightersOwned) {

			GameObject newFighter = Instantiate (FighterPrefab);
			newFighter.SetActive (true);


			newFighter.GetComponent <FighterModel> ().FighterData = fighter;
			newFighter.GetComponent <FighterModel> ().allegiance = FighterModel.FighterAlliegiance.Ally;

			newFighter.transform.position = startPos;

			startPos = new Vector3 (-5f - 1f, 1f, 0f);
		}

		StageData testData = new StageData ();

		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 200;
		testFighter1.ATK = 50;
		testFighter1.name = "Kalaban";

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

	public void OnBackButtonClicked ()
	{
		Application.LoadLevel("MainMenuScene");
	}
}

