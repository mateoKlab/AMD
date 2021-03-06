using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// Container class for XML Serialization.
//[XmlRoot]
//public class StageDatabase {
//
//	[XmlElement ("Stages")]
//	public List<StageData> stages;
//}

public enum StageType {
	Tournament,
	Mission,
	Event
}

[XmlRoot]
public class StageData {

	[XmlElement]
	public StageType stageType;

	[XmlElement]
	public string id;

	[XmlElement]
	public string name;

	[XmlArray("Enemies")]
	[XmlArrayItem("Fighter")]
	public List<FighterData> enemies = new List<FighterData> ();

	[XmlElement]
	public int goldReward;

	[XmlElement]
	public int xpReward;
}
