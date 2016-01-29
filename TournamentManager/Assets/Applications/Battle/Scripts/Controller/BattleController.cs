using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class BattleController : Controller<Battle, BattleModel, BattleView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public BattleEndExpController battleEndExpController { get; private set; }
    
    [Inject]
    public BattleEndController battleEndController { get; private set; }
    
    [Inject]
    public BattleMenuController battleMenuController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
    public GameObject meleeFighterPrefab;
	public GameObject archerFighterPrefab;
	public GameObject mageFighterPrefab;

	public GameObject enemyHPBarPrefab;

	public List<GameObject> allySpawnPos;
	public List<GameObject> enemySpawnPos;

	private Dictionary<Class, GameObject> prefabs = new Dictionary<Class, GameObject> ();
	
    void Start()
    {
        Messenger.AddListener(EventTags.FIGHTER_KILLED, FighterKilled);
		Messenger.AddListener(EventTags.FIGHTER_DEATH, DestroyFighter);
        Messenger.AddListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);

		prefabs.Add (Class.Warrior, meleeFighterPrefab);
		prefabs.Add (Class.Mage, mageFighterPrefab);
		prefabs.Add (Class.Archer, archerFighterPrefab);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(EventTags.FIGHTER_KILLED, FighterKilled);
		Messenger.RemoveListener(EventTags.FIGHTER_DEATH, DestroyFighter);
        Messenger.RemoveListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);
    }


	public void StartBattle ()
	{
		foreach (GameObject fighter in model.allies) {
			fighter.GetComponent<FighterController> ().StartBattle ();
		}

		foreach (GameObject fighter in model.enemies) {
			fighter.GetComponent<FighterController> ().StartBattle ();
		}
	}

	public void SpawnFighters()
	{
		GameObject newFighter = null;
		StageData currentStage = GameData.instance.currentStage;
		
		foreach (FighterData fighter in GameData.instance.GetActiveParty())
		{
			newFighter = SpawnFighter(fighter, FighterAlliegiance.Ally);
		}
		
		foreach (FighterData fighter in currentStage.enemies)
		{
			newFighter = SpawnFighter(fighter, FighterAlliegiance.Enemy);
		}

		battleMenuController.SetFighters(model.allies);
		battleMenuController.SetEnemies(model.enemies);

		// TEST. Disable.
//		SetBackgroundImage(currentStage);
	}

	public void SetBackgroundImage (StageData sData)
	{
		if (sData.stageType == StageType.Mission) 
		{
			view.backgroundImage.sprite = Resources.Load("Sprites/StageBackgrounds/" + sData.name.Split(' ')[0], typeof(Sprite)) as Sprite; 
		}
	}

	#region Events
    // Called when a Fighter is killed.
    // args[0]: typeof:GameObject  desc: The fighter killed.
    // args[1]: typeof:GameObject  desc: The attacker.
    void FighterKilled(params object[] args)
    {

        GameObject defender = (GameObject) args[0];
        GameObject attacker = (GameObject) args[1];

		defender.GetComponent<BoxCollider2D> ().enabled = false;
		defender.GetComponent<Rigidbody2D> ().constraints = RigidbodyConstraints2D.FreezeAll;

        if (defender.GetComponent<FighterModel>().allegiance == FighterAlliegiance.Ally)
        {
            model.allies.Remove(defender);

			foreach (GameObject enemy in model.enemies) {
				FighterController enemyController = enemy.GetComponent<FighterController> ();
				enemyController.RemoveFromRange (defender);
			}
        }
        else
        {
            model.enemies.Remove(defender);

			foreach (GameObject ally in model.allies) {
				FighterController allyController = ally.GetComponent<FighterController> ();
				allyController.RemoveFromRange (defender);
			}
        }

        // TODO: Give XP to attacker.

        if (model.enemies.Count == 0)
        {
			battleEndController.ShowBattleEndPopUp(true);

			if (GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount && GameData.instance.currentStage.stageType == StageType.Tournament)
            {
                GameData.instance.playerData.tournamentProgress++;
            }

            //Application.LoadLevel("MainMenuScene");
        }
		else if (model.allies.Count == 0)
        {
            // TODO: Apply injuries, etc.

			battleEndController.ShowBattleEndPopUp(false);
        }

        // TODO: pooling
//        Destroy(defender);
    }

	void DestroyFighter (params object[] args)
	{
		Destroy ((GameObject)args [0]);
	}

	public void OnProjectileHit (Attack attackData, GameObject defender)
	{
		FighterController attackingUnit = ((FighterController) attackData.attackOrigin.GetComponent<FighterController>());
		
		// Check if the defending unit is still alive. (Attacks may land at the same time).
		if (defender != null) {
			FighterController defendingUnit = ((FighterController)defender.GetComponent<FighterController> ());
			
			defendingUnit.OnReceiveAttack (attackData);
		}
	}
	
	public void OnMeleeAttack (Attack attackData)
	{
		if (attackData == null) {
			Debug.Log ("NULL ATTACK DATA");
		}
		
		FighterController attackingUnit = ((FighterController) attackData.attackOrigin.GetComponent<FighterController>());
		FighterController targetUnit    = ((FighterController) attackData.attackTarget.GetComponent<FighterController>());
		
		// Check if the defending unit is still alive.
		if (targetUnit.gameObject != null) {
			
			targetUnit.OnReceiveAttack (attackData);
		}
		SoundManager.instance.PlayAttackSFX();
	}
	
	public void OnRangedAttack(Attack attackData, Vector3 spawnPosition)
	{
		GameObject newProjectile = ProjectileManager.instance.GetProjectile(attackData, ProjectileType.Fireball); // Type = Temporary.
		newProjectile.transform.position = spawnPosition;
	}
	
	public void OnBackButtonClicked()
	{
		Application.LoadLevel("MainMenuScene");
	}

	public void OnEndScreenExp(params object[] args)
	{
		battleEndExpController.Show();
	}
	#endregion


	// TODO: clean up. encapsulate.
    private GameObject SpawnFighter(FighterData fighterData, FighterAlliegiance allegiance)
    {
        GameObject newFighter;

        // Instantiate prefab with appropriate behavior. (Melee Fighter Controller/Ranged Fighter Controller)
		newFighter = Instantiate (prefabs [fighterData.fighterClass]);

        FighterModel fighterModel = newFighter.GetComponent <FighterModel>();
        fighterModel.fighterData = fighterData;
        fighterModel.allegiance = allegiance;

		// Set currentHP back to max value.
		fighterModel.fighterData.HP = fighterModel.fighterData.maxHP;

        if (allegiance == FighterAlliegiance.Ally)
        {
            model.allies.Add(newFighter);
        }
        else
        {
            model.enemies.Add(newFighter);

			// Set layer of object (rigidBody) and child (trigger collider) to "EnemyUnits".
            newFighter.layer = LayerMask.NameToLayer("EnemyUnits");
			newFighter.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer("EnemyUnits");

			// Set enemy sprites to face the opposite direction.
			Vector3 tempScale = newFighter.transform.localScale;
			tempScale.x *= -1;
			newFighter.transform.localScale = tempScale;

			GameObject go = GameObject.Instantiate(enemyHPBarPrefab);
			battleMenuController.enemyHPItems.Add(go.GetComponent<EnemyHPBarScript>());
			//transform.root.GetComponent<Battle>().controller.battleMenuController.enemyHPItems.AttachToFighter(transform.root.GetComponent<BattleModel>().enemies[i]);
			go.transform.SetParent(battleMenuController.transform.parent, false);


        }

		SetStartingPosition (newFighter);

        newFighter.SetActive(true);

		//TEST.
		newFighter.GetComponent <FighterView> ().SetFighterSkin (fighterModel.fighterData.skinData);

        return newFighter;
    }

	private void SetStartingPosition (GameObject fighter)
	{
		FighterAlliegiance allegiance = fighter.GetComponent<FighterModel> ().allegiance;

		if (allegiance == FighterAlliegiance.Ally) {
			int index = model.allies.IndexOf (fighter);

			// TEMPORARY SPAWN POSITIONING.
			fighter.transform.position = allySpawnPos[0].transform.position; //new Vector3 (alliedStartingPos.x + (0.8f * index), -1f, fighter.transform.position.z);
			allySpawnPos.RemoveAt (0);

		} else {
			int index = model.enemies.IndexOf (fighter);

			fighter.transform.position = enemySpawnPos[0].transform.position; //new Vector3 (enemyStartingPos.x - (0.8f * index), -1f, fighter.transform.position.z);
			enemySpawnPos.RemoveAt (0);
		}
	}


}

