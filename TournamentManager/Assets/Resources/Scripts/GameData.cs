using UnityEngine;
//using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class GameData : MonoBehaviour {

	public const int MAX_ACTIVE_FIGHTERS = 6;

	public PlayerData playerData;
    public TownData town;


	public List<FighterData> fighterDatabase = new List<FighterData> ();
	public Dictionary<StageType, Dictionary<string, StageData>> stageDatabase;

	// List of sprites available for each sprite attachment/body part.
	private SpriteDatabase _spriteDatabase;
	public SpriteDatabase spriteDatabase { get; private set; }


	// Team management
	public FighterData[] activeParty = new FighterData[GameData.MAX_ACTIVE_FIGHTERS];
	public int currentPartyCost;

	// For test purposes only -AJ
	public StageData currentStage;

	private bool firstInstance = false;

	// Singleton instance. _instance will be set on Awake().
	private static GameData _instance;
	public static GameData instance {
		get { 
			if (!_instance) {
				GameObject newObject = new GameObject ();
				newObject.name = "GameData";
				newObject.AddComponent<GameData> ();

				_instance = newObject.GetComponent<GameData> ();
			}
			return _instance; 
		}
		private set { }
	}
	
	void Awake()
	{
		_instance = this;
		
		// Keep object alive between scenes.
		DontDestroyOnLoad(gameObject);
		
		// If there is more than 1 of this object, destroy the second instance.
		if (!firstInstance && FindObjectsOfType (typeof(GameData)).Length > 1) {
			DestroyImmediate (gameObject);
		} else {
			firstInstance = true;
		}

		LoadDatabase ();
		playerData = PlayerData.Load ();
        town = playerData.town;
		//LoadActiveFighters();
        LoadActiveParty();
	}

	public void Save ()
	{
		// Do Save.
	}
	
	public void LoadDatabase()
	{
		spriteDatabase = SpriteDatabase.LoadDatabase ();

		// Initialize stage Database dictionary.
		stageDatabase = new Dictionary<StageType, Dictionary<string, StageData>> ();
		foreach (StageType stageType in Enum.GetValues (typeof(StageType))) {
			stageDatabase.Add(stageType, new Dictionary<string, StageData>());
		}

		// Load Stage data.
		XmlSerializer ser = new XmlSerializer(typeof(StageDatabase));

		TextAsset textAsset = Resources.Load ("Data/StageDatabase") as TextAsset;
		System.IO.StringReader stringReader = new System.IO.StringReader(textAsset.text);
		StageDatabase stages;

		using (XmlReader reader = XmlReader.Create(stringReader))
		{
			stages = (StageDatabase) ser.Deserialize(reader);
		}

		foreach (StageData stage in stages.stages) {
			stageDatabase[stage.stageType].Add(stage.name, stage);
		}

//		// TEMPORARY DATABASE.
//		ser = new XmlSerializer(typeof(FighterDatabase));
//		textAsset = Resources.Load ("Data/FighterDatabase") as TextAsset;
//		stringReader = new System.IO.StringReader(textAsset.text);
//		FighterDatabase fighters;
//
//		using (XmlReader reader = XmlReader.Create(stringReader))
//		{
//			fighters = (FighterDatabase) ser.Deserialize(reader);
//		}
//
//		fighterDatabase = fighters.fighters;
	}

	#region Player Data Manipulation

	public void SavePlayerData()
	{
		playerData.Save();
	} 

	public List<FighterData> GetFightersOwned()
	{
		return playerData.fightersOwned;
	}

	public FighterData[] GetActiveParty()
	{
		return activeParty;
	}

	public int GetPartyCapacity()
	{
		return playerData.partyCapacity;
	}

	public bool IsWithinPartyCapacity(int troopCost)
	{
		return ((currentPartyCost + troopCost) <= playerData.partyCapacity);
	}

	// Return true if fighter successfully added to team, else return false
	public bool AddFighter(FighterData fighter)
	{
		if(playerData.fightersOwned.Count >= playerData.teamCapacity)
			return false;

//		fighter.activeTroopIndex = -1;
		playerData.fightersOwned.Add(fighter);
		playerData.Save ();

		return true;
	}

    public FighterData GetFighterByID(string id)
    {
        for(int i = 0; i < playerData.fightersOwned.Count; i++)
        {
           if(playerData.fightersOwned[i].id == id)
                return playerData.fightersOwned[i];
        }

        return null;
    }

    public bool CheckIfFighterActive(FighterData fd)
    {
        for(int i = 0; i < activeParty.Length; i++)
        {
            if(activeParty[i] != null && activeParty[i].id == fd.id)
            {
                return true;
            }
        }

        return false;
    }

    public int GetActiveFighterIndexOnParty(FighterData fd)
    {
        for(int i = 0; i < activeParty.Length; i++)
        {
            if(activeParty[i] != null && activeParty[i].id == fd.id)
            {
                return i;
            }
        }
        
        return -1;
    }

    public void SetFighterOnActiveParty(FighterData fighter, int indexOnParty) 
    {
        if(indexOnParty > -1 && indexOnParty < playerData.activePartyIDs.Length)
        {
            if(fighter == null)
            {
                activeParty[indexOnParty] = null;
                playerData.activePartyIDs[indexOnParty] = "";
            }
            else
            {
                activeParty[indexOnParty] = fighter;
                playerData.activePartyIDs[indexOnParty] = fighter.id;
                //Debug.Log("Added fighter " + playerData.activePartyIDs[indexOnParty] + " to party");
            }
        }
        else
        {
            Debug.LogError("Index out of bounds, failed to set fighter on party");
        }
    }

    public void LoadActiveParty()
    {
        if(playerData.fightersOwned.Count == 0)
        {
            InitFirstFighter();
        }

        for(int i = 0; i < activeParty.Length; i++)
        {
            activeParty[i] = null;

            if(!string.IsNullOrEmpty(playerData.activePartyIDs[i]))
            {
                FighterData fd = GetFighterByID(playerData.activePartyIDs[i]);
                if(fd != null)
                {
                    activeParty[i] = fd;
                }
            }
        }
    }

    public void InitFirstFighter()
    {
        // Create default character
        FighterData fd = FighterGenerator.GenerateFighter ();
        AddFighter(fd);
        SetFighterOnActiveParty(fd, 0);
        playerData.Save();
    }

	#endregion
}
