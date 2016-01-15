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

	private Dictionary<Class, GameObject> prefabs = new Dictionary<Class, GameObject> ();

	private Vector3 alliedStartingPos = new Vector3 (-5f, -1f, -1f);
	private Vector3 enemyStartingPos = new Vector3 (5f, -1f, -1f);

    void Start()
    {
        Messenger.AddListener(EventTags.FIGHTER_KILLED, FighterKilled);
        Messenger.AddListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);

		prefabs.Add (Class.Warrior, meleeFighterPrefab);
		prefabs.Add (Class.Mage, mageFighterPrefab);
		prefabs.Add (Class.Archer, archerFighterPrefab);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(EventTags.FIGHTER_KILLED, FighterKilled);
        Messenger.RemoveListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);
    }


	public void StartBattle ()
	{
		foreach (GameObject fighter in model.allies) {
			FighterStateContext state = fighter.GetComponent<FighterController> ().state;

			state.Walk ();
		}

		foreach (GameObject fighter in model.enemies) {
			FighterStateContext state = fighter.GetComponent<FighterController> ().state;
			
			state.Walk ();
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
		SetBackgroundImage(currentStage);
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

        if (defender.GetComponent<FighterModel>().allegiance == FighterAlliegiance.Ally)
        {
            model.allies.Remove(defender);
        }
        else
        {
            model.enemies.Remove(defender);
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
        Destroy(defender);
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
	
	public void OnRangedAttack(Attack attackData)
	{
		GameObject newProjectile = ProjectileManager.instance.GetProjectile(attackData, ProjectileType.Fireball); // Type = Temporary.
		newProjectile.transform.position = attackData.attackOrigin.transform.position;
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
        }

		// TEMP. Increase/Decrease box collider height to offset sprite positions.
		SetOffset (newFighter);
		SetStartingPosition (newFighter);


        newFighter.SetActive(true);

        return newFighter;
    }

	private void SetStartingPosition (GameObject fighter)
	{
	
		FighterAlliegiance allegiance = fighter.GetComponent<FighterModel> ().allegiance;

		if (allegiance == FighterAlliegiance.Ally) {
			int index = model.allies.IndexOf (fighter);

			fighter.transform.position = new Vector3 (alliedStartingPos.x + (0.8f * index), -1f, fighter.transform.position.z);

		} else {
			int index = model.enemies.IndexOf (fighter);

			fighter.transform.position = new Vector3 (enemyStartingPos.x - (0.8f * index), -1f, fighter.transform.position.z);
		}
	}

	private void SetOffset (GameObject fighter)
	{
		float randomOffset = Mathf.Round((UnityEngine.Random.Range (0.5f, 1.7f)) * 100f) / 100f; // Random float round off to 2 decimal place.
		
		BoxCollider2D collider = fighter.GetComponent <BoxCollider2D> ();
		collider.size = new Vector2 (collider.size.x, randomOffset);
		
		Vector3 tempPosition = fighter.transform.position;
		tempPosition.z = randomOffset * 10f;
		fighter.transform.position = tempPosition;
	}
}

