using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class GameData : MonoBehaviour {

	public const int MAX_ACTIVE_FIGHTERS = 5;

	public PlayerData playerData;

	public List<FighterData> fighterDatabase = new List<FighterData> ();
	public Dictionary<StageType, Dictionary<string, StageData>> stageDatabase;

	// Team management
	public FighterData[] activeFighters = new FighterData[GameData.MAX_ACTIVE_FIGHTERS];
	public int currentTeamCapacity;

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

		playerData = PlayerData.Load ();
		LoadDatabase ();
		LoadActiveFighters();
	}



	public void Save ()
	{
		// Do Save.
	}
	
	public void LoadDatabase()
	{

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

		ser = new XmlSerializer(typeof(FighterDatabase));
		textAsset = Resources.Load ("Data/FighterDatabase") as TextAsset;
		stringReader = new System.IO.StringReader(textAsset.text);
		FighterDatabase fighters;

		using (XmlReader reader = XmlReader.Create(stringReader))
		{
			fighters = (FighterDatabase) ser.Deserialize(reader);
		}

		fighterDatabase = fighters.fighters;
	}

	#region Player Data Manipulation

	public void SavePlayerData()
	{
		playerData.Save();
	}

	public void LoadActiveFighters()
	{
		for(int i = 0; i < playerData.fightersOwned.Count; i++)
		{
			if(playerData.fightersOwned[i].activeTroopIndex > -1 && isWithinTroopCapacity(playerData.fightersOwned[i].cost))
			{
				activeFighters[playerData.fightersOwned[i].activeTroopIndex] = playerData.fightersOwned[i];
				currentTeamCapacity += playerData.fightersOwned[i].cost;
			}
		}
	}

	public List<FighterData> GetFightersOwned()
	{
		return playerData.fightersOwned;
	}

	public FighterData[] GetActiveFighters()
	{
		return activeFighters;
	}

	public bool isWithinTroopCapacity(int troopCost)
	{
		return ((currentTeamCapacity + troopCost) <= playerData.troopCapacity);
	}

	#endregion
}
