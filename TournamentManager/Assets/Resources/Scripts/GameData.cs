using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class GameData : MonoBehaviour {
	
	public PlayerData PlayerData = new PlayerData ();

	public List<FighterData> FighterDatabase;

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
		
		// TEST CODE: Save TEST DATA
		SaveTestData ();
	
		LoadTestData ();
	}

	void Load ()
	{



		// Load Fighters data.
	}

	public void Save ()
	{

	
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
		
		StageData testData = new StageData ();
		testData.enemies.Add (testEnemy1);
		testData.enemies.Add (testEnemy2);
		
		XmlSerializer xmls = new XmlSerializer(typeof(StageData));
		
		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/StageDatabase.xml", FileMode.OpenOrCreate))
		{
			xmls.Serialize(stream, testData);
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
		fighterDatabase.Fighters = new List<FighterData> ();

		fighterDatabase.Fighters.Add (testFighter1);
		fighterDatabase.Fighters.Add (testFighter2);
		fighterDatabase.Fighters.Add (testFighter3);

		xmls = new XmlSerializer(typeof(FighterDatabase));
		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/FighterDatabase.xml", FileMode.OpenOrCreate))
		{
			xmls.Serialize(stream, fighterDatabase);
		}

		// Save changes to xml files.
		AssetDatabase.Refresh();
	}

	public void LoadTestData()
	{
		// Load Stage data.
		XmlSerializer ser = new XmlSerializer(typeof(StageData));
		StageData stageData;
		
		TextAsset textAsset = Resources.Load ("Data/StageDatabase") as TextAsset;
		System.IO.StringReader stringReader = new System.IO.StringReader(textAsset.text);
		
		using (XmlReader reader = XmlReader.Create(stringReader))
		{
			stageData = (StageData) ser.Deserialize(reader);
		}
	}
}
