﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// Container class for FighterData XML Serialization.
[XmlRoot]
public class FighterDatabase {

	[XmlElement ("Fighters")]
	public List<FighterData> fighters;
}

public enum FighterElement {
	Fire,
	Water,
	Lightning,
	Earth
}

[XmlRoot]
public class FighterData {

	[XmlElement]
	public int HP = 1000;

	[XmlElement]
	public int ATK = 100;

	[XmlElement ("Name")]
	public string name = "Juan";

	[XmlElement ("Class")]
	public string fighterClass = "Warrior";

	[XmlElement ("Element")]
	public FighterElement fighterElement;

	[XmlElement ("Cost")]
	public int cost = 2;

	[XmlElement ("ActiveIndex")]
	public int activeTroopIndex = -1;
}
