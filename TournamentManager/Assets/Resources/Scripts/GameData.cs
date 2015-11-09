﻿using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class GameData : MonoBehaviour {
	
	public PlayerData PlayerData;

	public List<FighterData> fighterDatabase = new List<FighterData> ();
	public Dictionary<StageType, Dictionary<string, StageData>> stageDatabase;

	private bool firstInstance = false;
	
	// Singleton instance. _instance will be set on Awake().
	private static GameData _instance;
	public static GameData Instance {
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

		// Create TEST Database.
		SaveTestData ();	

		PlayerData = PlayerData.Load ();
		LoadDatabase ();
	}



	public void Save ()
	{
		// Do Save.
	}


	public void SaveTestData ()
	{
		// TEST DATA
		FighterData testEnemy1 = new FighterData ();
		testEnemy1.HP = 1000;
		testEnemy1.ATK = 150;
		
		FighterData testEnemy2 = new FighterData ();
		testEnemy2.HP = 2000;
		testEnemy2.ATK = 100;
		
		StageData testStage1 = new StageData ();
		testStage1.stageType = StageType.Tournament;
		testStage1.name = "Tournament 1";
		testStage1.enemies.Add (testEnemy1);
		testStage1.enemies.Add (testEnemy2);

		StageData testStage2 = new StageData ();
		testStage2.stageType = StageType.Quest;
		testStage2.name = "Test Quest 1";
		testStage2.enemies.Add (testEnemy1);
		testStage2.enemies.Add (testEnemy2);

		StageData testStage3 = new StageData ();
		testStage3.stageType = StageType.Quest;
		testStage3.name = "Test Quest 2";
		testStage3.enemies.Add (testEnemy1);
		testStage3.enemies.Add (testEnemy2);

		StageData testStage4 = new StageData ();
		testStage4.stageType = StageType.Quest;
		testStage4.name = "Test Quest 3";
		testStage4.enemies.Add (testEnemy1);
		testStage4.enemies.Add (testEnemy2);

		StageDatabase stageDatabase = new StageDatabase ();

		stageDatabase.stages = new List<StageData> ();
		stageDatabase.stages.Add (testStage1);
		stageDatabase.stages.Add (testStage2);
		stageDatabase.stages.Add (testStage3);
		stageDatabase.stages.Add (testStage4);
		
		XmlSerializer xmls = new XmlSerializer(typeof(StageDatabase));
		
		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/StageDatabase.xml", FileMode.OpenOrCreate))
		{
			xmls.Serialize(stream, stageDatabase);
		}

		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 1000;
		testFighter1.ATK = 150;
		testFighter1.name = "AJ";

		FighterData testFighter2 = new FighterData ();
		testFighter2.HP = 2000;
		testFighter2.ATK = 100;
		testFighter2.name = "Dave";

		FighterData testFighter3 = new FighterData ();
		testFighter2.HP = 1500;
		testFighter2.ATK = 100;
		testFighter2.name = "Matt";

		FighterDatabase fighterDatabase = new FighterDatabase ();
		fighterDatabase.fighters = new List<FighterData> ();

		fighterDatabase.fighters.Add (testFighter1);
		fighterDatabase.fighters.Add (testFighter2);
		fighterDatabase.fighters.Add (testFighter3);

		xmls = new XmlSerializer(typeof(FighterDatabase));
		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/FighterDatabase.xml", FileMode.OpenOrCreate))
		{
			xmls.Serialize(stream, fighterDatabase);
		}

		// Save changes to xml files.
		AssetDatabase.Refresh();
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
}
