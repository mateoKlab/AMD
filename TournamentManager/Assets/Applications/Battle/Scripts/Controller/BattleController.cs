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

    [HideInInspector]
    public List<GameObject>
        allies = new List<GameObject>();
    [HideInInspector]
    public List<GameObject>
        enemies = new List<GameObject>();

    void Start()
    {
        Messenger.AddListener(EventTags.FIGHTER_KILLED, FighterKilled);
        Messenger.AddListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(EventTags.FIGHTER_KILLED, FighterKilled);
        Messenger.RemoveListener(EventTags.END_SCREEN_EXP, OnEndScreenExp);
    }

    // Called when a Fighter is killed.
    // args[0]: typeof:GameObject  desc: The fighter killed.
    // args[1]: typeof:GameObject  desc: The attacker.
    void FighterKilled(params object[] args)
    {

        GameObject defender = (GameObject) args[0];
        GameObject attacker = (GameObject) args[1];

        if (defender.GetComponent<FighterModel>().allegiance == FighterAlliegiance.Ally)
        {
            allies.Remove(defender);
        }
        else
        {
            enemies.Remove(defender);
        }

        // TODO: Give XP to attacker.

        if (enemies.Count == 0)
        {
			battleEndController.ShowBattleEndPopUp(true);

			if (GameData.instance.playerData.tournamentProgress < GameData.instance.playerData.tournamentMatchCount && GameData.instance.currentStage.stageType == StageType.Tournament)
            {
                GameData.instance.playerData.tournamentProgress++;
            }

            //Application.LoadLevel("MainMenuScene");
        }
        else if (allies.Count == 0)
        {
            // TODO: Apply injuries, etc.

			battleEndController.ShowBattleEndPopUp(false);
        }

        // TODO: pooling
        Destroy(defender);
    }
			                     
    public void SpawnFighters()
    {
        GameObject newFighter;

        // TEST spawn positions. TODO: positioning code.
        Vector3 startPos = new Vector3(-2f, -1f, -1f);
        
        foreach (FighterData fighter in GameData.instance.GetActiveParty())
        {
            if (fighter == null) {
                continue;
			}

            newFighter = SpawnFighter(fighter, FighterAlliegiance.Ally);

            startPos = new Vector3(startPos.x - 1f, - 1f, newFighter.transform.position.z);
			newFighter.transform.position = startPos;
        }

        startPos = new Vector3(2f, -1f, -1f);

        StageData currentStage = GameData.instance.currentStage;
        foreach (FighterData fighter in currentStage.enemies)
        {
            newFighter = SpawnFighter(fighter, FighterAlliegiance.Enemy);

			startPos = new Vector3(startPos.x + 1f, -1f, newFighter.transform.position.z);
			newFighter.transform.position = startPos;
        }


        battleMenuController.SetFighters(allies);
		SetBackgroundImage(currentStage);
    }

	public void SetBackgroundImage(StageData sData) {
		if (sData.stageType == StageType.Mission) 
		{
			view.backgroundImage.sprite = Resources.Load("Sprites/StageBackgrounds/" + sData.name.Split(' ')[0], typeof(Sprite)) as Sprite; 
		}
	}

    public void OnRangedAttack(Attack attackData, GameObject defender)
    {
		FighterController attackingUnit = ((FighterController) attackData.attackOrigin.GetComponent<FighterController>());

		// Check if the defending unit is still alive. (Attacks may land at the same time).
		if (defender != null) {
			FighterController defendingUnit = ((FighterController)defender.GetComponent<FighterController> ());
		
			defendingUnit.OnReceiveAttack (attackData);
		}
    }

	public void OnMeleeAttack(Attack attackData)
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

	// TODO: clean up. encapsulate.
    private GameObject SpawnFighter(FighterData fighterData, FighterAlliegiance allegiance)
    {
        GameObject newFighter;

        // Instantiate prefab with appropriate behavior. (Melee Fighter Controller/Ranged Fighter Controller)
        if (fighterData.fighterClass == Class.Archer)
        {
            newFighter = Instantiate(archerFighterPrefab);
		}
		else if(fighterData.fighterClass == Class.Mage)
		{
			newFighter = Instantiate(mageFighterPrefab);
		}
        else
        {
            newFighter = Instantiate(meleeFighterPrefab);
        }

        FighterModel fighterModel = newFighter.GetComponent <FighterModel>();
        fighterModel.fighterData = fighterData;
        fighterModel.allegiance = allegiance;

		// Set currentHP back to max value.
		fighterModel.fighterData.HP = fighterModel.fighterData.maxHP;

		// TEMP. Increase/Decrease box collider height to offset sprite positions.

		float randomOffset = Mathf.Round((UnityEngine.Random.Range (0.5f, 1.7f)) * 100f) / 100f; // Random float round off to 2 decimal place.


		BoxCollider2D collider = newFighter.GetComponent <BoxCollider2D> ();
		collider.size = new Vector2 (collider.size.x, randomOffset);

		Vector3 tempPosition = newFighter.transform.position;
		tempPosition.z = randomOffset * 10f;
		newFighter.transform.position = tempPosition;

//		Debug.Log ("RANDOM OFFSET: " + randomOffset.ToString ());

        if (allegiance == FighterAlliegiance.Ally)
        {
            allies.Add(newFighter);
        }
        else
        {
            enemies.Add(newFighter);

			// Set layer of object (rigidBody) and child (trigger collider) to "EnemyUnits".
            newFighter.layer = LayerMask.NameToLayer("EnemyUnits");
			newFighter.transform.GetChild (0).gameObject.layer = LayerMask.NameToLayer("EnemyUnits");

			// Set enemy sprites to face the opposite direction.
			Vector3 tempScale = newFighter.transform.localScale;
			tempScale.x *= -1;
			newFighter.transform.localScale = tempScale;
        }

//		newFighter.GetComponent <FighterController> ().SetFighterSkin ();
        newFighter.SetActive(true);

        return newFighter;
    }

    public void OnEndScreenExp(params object[] args)
    {
        battleEndExpController.Show();
    }
}

