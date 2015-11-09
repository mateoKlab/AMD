using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot]
public class StageDatabase {

	[XmlElement ("Stages")]
	public List<StageData> stages;
}

public enum StageType {
	Tournament,
	Quest,
	Event
}

[XmlRoot]
public class StageData {

	[XmlElement]
	public StageType stageType;

	[XmlElement]
	public string name;

	[XmlArray("Enemies")]
	[XmlArrayItem("Fighter")]
	public List<FighterData> enemies = new List<FighterData> ();
}
