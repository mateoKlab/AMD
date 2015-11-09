using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// Container class for FighterData XML Serialization.
[XmlRoot]
public class FighterDatabase {

	[XmlElement ("Fighter")]
	public List<FighterData> fighters;
}

[XmlRoot]
public class FighterData {

	[XmlElement]
	public int HP = 1000;

	[XmlElement]
	public int ATK = 100;

	[XmlElement]
	public string name;
}
