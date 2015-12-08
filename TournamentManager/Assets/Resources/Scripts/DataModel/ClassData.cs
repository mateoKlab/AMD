using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public enum Class
{
	Warrior,
	Mage,
	Archer
}

// Info and base statistics of each class.
public class ClassData {

	[XmlElement ("Class")]
	public Class fighterClass;

	[XmlElement ("EquipmentAllowed")]
	public List<Equipment.Type> equipmentAllowed;

	public int baseATK;

	public int baseHP;

	public int baseDEF;
}
