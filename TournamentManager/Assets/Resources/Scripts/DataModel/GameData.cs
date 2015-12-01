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
        playerData.Save();
	}
	
	public void LoadDatabase()
	{
		GameDatabase.LoadGameDatabase ();
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

    // Base fighter capacity + barracks facility level
    public int GetFighterCapacity()
    {
        return (playerData.fighterCapacity + town.AdditionalFighterCapacity());
    }

	public FighterData[] GetActiveParty()
	{
		return activeParty;
	}

	public int GetPartyCapacity()
	{
		return (playerData.partyCapacity + town.AdditionalPartyCapacity());
	}

	public bool IsWithinPartyCapacity(int troopCost)
	{
		return ((currentPartyCost + troopCost) <= GetPartyCapacity());
	}

	// Return true if fighter successfully added to team, else return false
	public bool AddFighter(FighterData fighter)
	{
		if(playerData.fightersOwned.Count >= GetFighterCapacity())
        {
			return false;
        }

		playerData.fightersOwned.Add(fighter);
//		playerData.Save ();

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
