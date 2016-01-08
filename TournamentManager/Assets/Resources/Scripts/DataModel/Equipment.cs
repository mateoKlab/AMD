using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot ("Equipment")]
public struct Equipment {

	#region Class members
	[XmlElement ("EquipmentType")]
	public Type type; //Type.Weapon.sword;

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
	#endregion

	
	// Nested "Type" classes used as a hierarchical ID system. (C# does not support struct inheritance/nested enums.)
	// Usage: someEquip.type = Equipment.Type.Weapon.Sword;
	// Inherits from ItemType<T> to be enabled for use with the Inventory system.
	[XmlInclude(typeof(Weapon))]
	[XmlInclude(typeof(Offhand))]
	[XmlInclude(typeof(Body))]
	public class Type : ItemType<Equipment>
	{
		// Subtype static members. Allows use of Equipment.Type.<Subtype> "abstract" type.
		// "Type" suffix added to prevent hiding of nested subclasses.
		public static Type WeaponType  = new Type { typeName = "Weapon" };
		public static Type OffhandType = new Type { typeName = "Offhand" };
		public static Type BodyType	   = new Type { typeName = "Body" };
		
		
		// Equipment Types determine attachment type to sprite.
		#region Equipment Types
		public class Weapon : Type {
			
			// All weapon subtypes are of type: WeaponType.
			public override ItemType<Equipment> parentType {
				get { return Type.WeaponType; }
			}
			
			// Weapon subtypes.
			public static Weapon Sword = new Weapon { typeName = "Sword" };
			public static Weapon Staff = new Weapon { typeName = "Staff" };
		}
		
		public class Offhand : Type {
			
			// All Offhand subtypes are of type: OffhandType.
			public override ItemType<Equipment> parentType {
				get { return Type.OffhandType; }
			}
			
			// Offhand subtypes.
			public static Offhand Shield = new Offhand { typeName = "Shield" };
		}
		
		public class Body : Type {
			
			// All Body subtypes are of type: BodyType.
			public override ItemType<Equipment> parentType {
				get { return Type.BodyType; }
			}
			
			// Body subtypes.
			public static Body HeavyArmor = new Body { typeName = "HeavyArmor" };
		}
		#endregion
	}
}

