using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class PlayerData {

	[XmlElement ("Gold")]
	public int gold = 1000;

	[XmlElement ("Diamonds")]
	public int diamonds;

	[XmlElement ("TournamentProgress")]
	public int tournamentProgress;

	[XmlElement ("TournamentMatchCount")]
	public int tournamentMatchCount;

	[XmlArray("FightersOwned")]
	[XmlArrayItem("Fighter")]
	public List<FighterData> fightersOwned = new List<FighterData> ();
	
	[XmlElement("TroopCapacity")]
	public int troopCapacity = 10;

	//[XmlArrayItem("UnlockedStages")]
	//public List<string> unlockedStages = new List<string> ();

	public void Save ()
	{
		XmlSerializer xmls = new XmlSerializer(typeof(PlayerData));

		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/PlayerData.xml", FileMode.OpenOrCreate))
		{
			xmls.Serialize(stream, this);
		}

		AssetDatabase.Refresh();
	}

	public static PlayerData Load ()
	{

		XmlSerializer ser = new XmlSerializer(typeof(PlayerData));

		TextAsset textAsset = Resources.Load ("Data/PlayerData") as TextAsset;
		System.IO.StringReader stringReader;
		
		if (textAsset == null) {
			Debug.Log ("No Player save file found. Returning default values...");
			return new PlayerData ();
		} else {
			stringReader = new System.IO.StringReader(textAsset.text);
			using (XmlReader reader = XmlReader.Create(stringReader))
			{
				return (PlayerData) ser.Deserialize(reader);
			}
		}
	}
}
