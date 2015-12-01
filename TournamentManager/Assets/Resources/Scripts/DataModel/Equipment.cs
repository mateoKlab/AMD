using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

// Enums must match FighterSpriteAttachment type to be set properly.


[XmlRoot ("Equipment")]
public struct Equipment {

	public enum EquipmentType
	{
		Weapon,
		Mantle,
		Body
	}

	[XmlAttribute ("Type")]
	public EquipmentType type;// = EquipmentType.Weapon;

	[XmlElement ("ID")]
	public string id;// = "Default ID";

	[XmlElement ("Name")]
	public string name;// = "Default weapon";

	[XmlElement ("SpriteName")]
	public string spriteName;// = "default filename";

	[XmlElement ("Attack")]
	public float attack;// = 100.0f;

	[XmlElement ("Defense")]
	public float defense;// = 10.0f;

	[XmlElement ("HP")]
	public int hp;// = 200;

}




