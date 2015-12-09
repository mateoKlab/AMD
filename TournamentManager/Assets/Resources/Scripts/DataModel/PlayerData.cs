using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class PlayerData
{
    [XmlElement ("Gold")]
    public int
        gold = 1000;

    [XmlElement ("Diamonds")]
    public int
        diamonds;

//	[XmlArray ("EquipmentUnlocked")]
//	[XmlArrayItem("Equip")]
//	public List<string> unlockedEquipment;

    [XmlElement ("TournamentProgress")]
    public int
        tournamentProgress;

    [XmlElement ("TournamentMatchCount")]
    public int
        tournamentMatchCount;

//	public List<string> unlockedEquipment 

    [XmlArray("ActiveParty")]
    [XmlArrayItem("ActiveFighter")]
    public string[] activePartyIDs = new string[GameData.MAX_ACTIVE_FIGHTERS] {"", "", "", "", "", ""};

    [XmlArray("FightersOwned")]
    [XmlArrayItem("Fighter")]
    public List<FighterData>
        fightersOwned = new List<FighterData>();
	
    [XmlElement("PartyCapacity")]
    public int
        partyCapacity = 20;

    /// <summary>
    /// The fighter base capacity.
    /// Do not access this directly. Use the GetFighterCapacity method on GameData instead
    /// </summary>
    [XmlElement("FighterCapacity")]
    public int
        fighterCapacity = 50;

    // TOWN
    [XmlElement("Town")]
    public TownData town;


    private PlayerData() 
    {
        town = new TownData();
    }

	void Start ()
	{


		//TEST
//		unlockedEquipment = new List<Equipment> ();

//		unlockedEquipment.Add (Equipment.Type.Weapon.Sword);
//		unlockedEquipment.Add (Equipment.Type.Armor.Body);
	}

    public void Save()
    {

		// TEST. Add unlocked equipment to player.
		Start ();
        XmlSerializer xmls = new XmlSerializer(typeof(PlayerData));

        #if UNITY_EDITOR || UNITY_IOS
		using(StringWriter sww = new StringWriter())
		using(XmlWriter writer = XmlWriter.Create(sww))
		{
			xmls.Serialize(writer, this);

			// Using XmlDocument guarantees a properly formatted xml file.
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(sww.ToString());
			xdoc.Save(Application.dataPath + "/Resources/Data/PlayerData.xml");
		}

        #elif UNITY_ANDROID
        string filePath = GetPath("PlayerData.xml");
        using (var stream = System.IO.File.CreateText(filePath))
        {
            xmls.Serialize(stream, this);
            stream.Close();
        }
        #endif

        #if UNITY_EDITOR
        AssetDatabase.Refresh();
        #endif
    }

    public static PlayerData Load()
    {
        XmlSerializer ser = new XmlSerializer(typeof(PlayerData));

        #if UNITY_EDITOR || UNITY_IOS
        TextAsset textAsset = Resources.Load("Data/PlayerData") as TextAsset;
        System.IO.StringReader stringReader;
		
        if (textAsset == null)
        {
            Debug.Log("No Player save file found. Returning default values...");
            return new PlayerData();
        }
        else
        {
            stringReader = new System.IO.StringReader(textAsset.text);
            using (XmlReader reader = XmlReader.Create(stringReader))
            {
                return (PlayerData) ser.Deserialize(reader);
            }
        }
        #elif UNITY_ANDROID
        string filePath = GetPath("PlayerData.xml");
        if (System.IO.File.Exists(filePath))
        {
            using (XmlReader reader = XmlReader.Create(filePath))
            {
                return (PlayerData) ser.Deserialize(reader);
            }
        }
        else
        {
            Debug.Log("No Player save file found. Returning default values...");
            return new PlayerData();
        }
        #endif
    }

    // Retrieve relative path depending on platform
    private static string GetPath(string fileName)
    {
        #if UNITY_EDITOR
        return Application.dataPath + "/Resources/Data/" + fileName;
        #elif UNITY_ANDROID
        return Application.persistentDataPath+fileName;
        #elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+fileName;
        #else
        return Application.dataPath +"/"+ fileName;
        #endif
    }
}
