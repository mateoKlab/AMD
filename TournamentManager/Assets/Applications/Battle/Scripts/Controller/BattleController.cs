using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class BattleController : Controller<Battle>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public BattleMenuController battleMenuController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public GameObject FighterPrefab;

	public List<GameObject> allies = new List<GameObject> ();
	public List<GameObject> enemies = new List<GameObject> ();
	
	void Start ()
	{
		Messenger.AddListener (EventTags.FIGHTER_KILLED, FighterKilled);
	}


	// Called when a Fighter is killed.
		// args[0]: typeof:GameObject  desc: The fighter killed.
		// args[1]: typeof:GameObject  desc: The attacker.
	void FighterKilled (params object[] args)
	{
		GameObject defender = (GameObject)args [0];
		GameObject attacker = (GameObject)args [1];

		if (defender.GetComponent<FighterModel> ().allegiance == FighterModel.FighterAlliegiance.Ally) {
			allies.Remove (defender);
		} else {
			enemies.Remove (defender);
		}

		// TODO: pooling
		Destroy (defender);

		// TODO: Give XP to attacker.

		if (enemies.Count == 0) {
			Debug.Log ("WIN");

			//TODO: Show WIN popup. Send WIN event.		
			//(model as BattleModel).currentStage.

			if (GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount)
			{
				GameData.instance.playerData.tournamentProgress++;
			}
			Application.LoadLevel ("MainMenuScene");
		} else if (allies.Count == 0) {
			Debug.Log ("LOSE");
			// TODO: Apply injuries, etc.

			Application.LoadLevel ("MainMenuScene");
		}

	}

	void OnDestroy ()
	{
//		Messenger.RemoveListener (EventTags.FIGHTER_KILLED, FighterKilled);
	}
			                     
	public void SpawnFighters ()
	{
		// TEST spawn positions.
		Vector3 startPos = new Vector3 (-5f, -1f, 0f);

		foreach (FighterData fighter in GameData.instance.playerData.fightersOwned) {
			GameObject newFighter = Instantiate (FighterPrefab);
			allies.Add (newFighter);
			newFighter.SetActive (true);


			newFighter.GetComponent <FighterModel> ().fighterData = fighter;
			newFighter.GetComponent <FighterModel> ().allegiance = FighterModel.FighterAlliegiance.Ally;

			newFighter.transform.position = startPos;

			startPos = new Vector3 (startPos.x -1f, - 1f, 0f);
		}

		startPos = new Vector3 (4f, -1f, 0f);

		StageData currentStage = GameData.instance.currentStage;
		foreach (FighterData fighter in currentStage.enemies) {

			GameObject newFighter = Instantiate (FighterPrefab);
			enemies.Add (newFighter);

			newFighter.layer = LayerMask.NameToLayer("EnemyUnits");
			newFighter.SetActive (true);
			
			
			newFighter.GetComponent <FighterModel> ().fighterData = fighter;
			newFighter.GetComponent <FighterModel> ().allegiance = FighterModel.FighterAlliegiance.Enemy;

			newFighter.transform.position = startPos;
			newFighter.transform.rotation = Quaternion.Euler(0,180f,0);

			startPos = new Vector3 (startPos.x + 1.2f, -1f, 0f);
		}

		// Set UI.
		List<FighterData> fighters = new List<FighterData> ();
		foreach (GameObject fighter in allies) {
			fighters.Add (fighter.GetComponent<FighterModel> ().fighterData);
		}

		battleMenuController.SetFighters (fighters);
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

