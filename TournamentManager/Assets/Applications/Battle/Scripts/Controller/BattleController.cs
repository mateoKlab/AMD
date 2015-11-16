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
    
	public GameObject meleeFighterPrefab;
	public GameObject rangedFighterPrefab;

	[HideInInspector]
	public List<GameObject> allies = new List<GameObject> ();
	[HideInInspector]
	public List<GameObject> enemies = new List<GameObject> ();

	void Start ()
	{
		Messenger.AddListener (EventTags.FIGHTER_KILLED, FighterKilled);
	}

	void OnDestroy ()
	{
		Messenger.RemoveListener (EventTags.FIGHTER_KILLED, FighterKilled);
	}

	// Called when a Fighter is killed.
		// args[0]: typeof:GameObject  desc: The fighter killed.
		// args[1]: typeof:GameObject  desc: The attacker.
	void FighterKilled (params object[] args)
	{

		GameObject defender = (GameObject)args [0];
		GameObject attacker = (GameObject)args [1];

		if (defender.GetComponent<FighterModel> ().allegiance == FighterAlliegiance.Ally) {
			allies.Remove (defender);
		} else {
			enemies.Remove (defender);
		}

		// TODO: Give XP to attacker.

		if (enemies.Count == 0) {

			//TODO: Show WIN popup. Send WIN event.		

			if (GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount)
			{
				GameData.instance.playerData.tournamentProgress++;
			}

			Application.LoadLevel ("MainMenuScene");
		} else if (allies.Count == 0) {
			// TODO: Apply injuries, etc.

			Application.LoadLevel ("MainMenuScene");
		}

		// TODO: pooling
		Destroy (defender);
	}
			                     
	public void SpawnFighters ()
	{
		GameObject newFighter;

		// TEST spawn positions. TODO: positioning code.
		Vector3 startPos = new Vector3 (-5f, -1f, -1f);

		foreach (FighterData fighter in GameData.instance.playerData.fightersOwned) {

			newFighter = SpawnFighter (fighter, FighterAlliegiance.Ally);
			newFighter.transform.position = startPos;

			startPos = new Vector3 (startPos.x -1f, - 1f, -1f);
		}

		startPos = new Vector3 (3f, -1f, -1f);

		StageData currentStage = GameData.instance.currentStage;
		foreach (FighterData fighter in currentStage.enemies) {
			newFighter = SpawnFighter (fighter, FighterAlliegiance.Enemy);

			newFighter.transform.position = startPos;
			newFighter.transform.rotation = Quaternion.Euler(0,180f,0);

			startPos = new Vector3 (startPos.x + 1.2f, -1f, -1f);
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
		
		defendingUnit.OnReceiveAttack (attackingUnit.GetAttackData ());
	}

	public void OnRangedAttack (GameObject attacker)
	{
		GameObject newProjectile = ProjectileManager.instance.GetProjectile (attacker, ProjectileType.Fireball); // Type = Temporary.
		newProjectile.transform.position = attacker.transform.position;
	}

	public void OnBackButtonClicked ()
	{
		Application.LoadLevel("MainMenuScene");
	}

	private GameObject SpawnFighter (FighterData fighterData, FighterAlliegiance allegiance)
	{
		GameObject newFighter;

		// Instantiate prefab with appropriate behavior. (Melee Fighter Controller/Ranged Fighter Controller)
		if (fighterData.isRanged) {
			newFighter = Instantiate (rangedFighterPrefab);
		} else {
			newFighter = Instantiate (meleeFighterPrefab);
		}

		FighterModel fighterModel = newFighter.GetComponent <FighterModel> ();
		fighterModel.fighterData  = fighterData;
		fighterModel.allegiance   = allegiance;

		if (allegiance == FighterAlliegiance.Ally) {
			allies.Add  (newFighter);
		} else {
			enemies.Add (newFighter);
			newFighter.layer = LayerMask.NameToLayer ("EnemyUnits");
		}

		newFighter.SetActive (true);

		return newFighter;
	}
}

