using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("Equipment")]
public struct Equipment : IDatabaseItem {

	#region Class members
	[XmlElement ("EquipmentType")]
	public string type; //Type.Weapon.sword;

	[XmlElement ("ID")]
	public string id;// = "Default ID";

	[XmlElement ("Name")]
	public string name;// = "Default weapon";
	
	[XmlElement ("Attack")]
	public float attack;// = 100.0f;

	[XmlElement ("Defense")]
	public float defense;// = 10.0f;

	[XmlElement ("HP")]
	public int hp;// = 200;

	[XmlArray ("Sprites")]
	[XmlArrayItem ("Sprite")]
	public List<EquipmentSprite> sprites;// = "default filename";
	#endregion

	// Implement IDatabaseItem property.
	string IDatabaseItem.itemId {
		get { return this.id; }
		set { this.id = value; }
	}
}

// Nested "Type" classes used as a hierarchical ID system. (C# does not support struct inheritance/nested enums.)
// Usage: someEquip.type = Equipment.Type.Weapon.Sword;
public class EquipmentType
{
	#region Equipment Types
	public class Weapon : EquipmentType {

		#region Weapon Types
		public class Sword : Weapon { }
		public class Staff : Weapon { }
		#endregion
	}

	public class Offhand : EquipmentType {

		// Offhand subtypes.
		public class Shield : Offhand { }
	}
	
	public class Armor : EquipmentType {

		// Armor subtypes.
		public class Body : Armor { }		
		public class Shoulder : Armor { }		
		public class Gauntlet : Armor { }
	}
	#endregion
}

[XmlRoot ("EquipmentSprite")]
public struct EquipmentSprite
{
	[XmlElement ("AttachmentType")]
	public string attachmentType;
	
	[XmlElement ("SpriteName")]
	public string spriteName;
}
