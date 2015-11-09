using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class PlayerData {

	[XmlElement ("Gold")]
	public int gold;

	[XmlElement ("Diamonds")]
	public int diamonds;

	[XmlArray("FightersOwned")]
	[XmlArrayItem("Fighter")]
	public List<FighterData> fightersOwned = new List<FighterData> ();


	public void Save ()
	{
		XmlSerializer xmls = new XmlSerializer(typeof(PlayerData));
		
		using(var stream = new FileStream(Application.dataPath + "/Resources/Data/PlayerData.xml", FileMode.Create))
		{
			xmls.Serialize(stream, this);
		}

		AssetDatabase.Refresh();

	}
}
