using UnityEngine;
//using UnityEditor;
using System;
using System.Linq;
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
//        LoadActiveParty();
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
		return playerData.fightersOwned.Values.ToList ();
	}

    // Base fighter capacity + barracks facility level
    public int GetFighterCapacity()
    {
        return (playerData.fighterCapacity + town.AdditionalFighterCapacity());
    }
	
	public List<FighterData> GetActiveParty()
	{
		List<FighterData> fighters = new List<FighterData> ();

		foreach (string id in playerData.currentParty.fighters) {
			fighters.Add (playerData.fightersOwned[id]);
		}

		return fighters;
	}

	// Return true if fighter successfully added to team, else return false
	public bool AddFighter(FighterData fighter)
	{
		if(playerData.fightersOwned.Count >= GetFighterCapacity())
        {
			return false;
        }

		playerData.fightersOwned.Add (fighter.id, fighter);
		playerData.Save ();

		return true;
	}

    public void InitFirstFighter()
    {
        // Create default character
        FighterData fd = FighterGenerator.GenerateFighter ();
        AddFighter(fd);
        playerData.Save();
    }

	#endregion
}
