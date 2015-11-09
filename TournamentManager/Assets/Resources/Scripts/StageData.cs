using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot]
public class StageData {

	[XmlArray("Enemies")]
	[XmlArrayItem("FighterData")]
	public List<FighterData> enemies = new List<FighterData> ();
}
