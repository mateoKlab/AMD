using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


[XmlRoot ("Equipment")]
public struct Equipment {

	#region Class members
	[XmlElement ("Type")]
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
	[XmlInclude(typeof(Weapon))]
	[XmlInclude(typeof(Offhand))]
	[XmlInclude(typeof(Body))]
	public class Type
	{
		[XmlElement ("TypeName")]
		public string typeName;

		// Equipment Types determine attachment type to sprite.
		#region Equipment Types
		public class Weapon : Type {
			
			public static Weapon Sword = new Weapon { typeName = "Sword" };
			public static Weapon Staff = new Weapon { typeName = "Staff" };
		}
		
		public class Offhand : Type {
			
			public static Offhand Shield = new Offhand { typeName = "Shield" };
		}
		
		public class Body : Type {
			
			public static Body HeavyArmor = new Body { typeName = "HeavyArmor" };
		}
		#endregion


		// Override Equals and GetHashCode methods to determine type equality.
		public override bool Equals(object obj)
		{
			if (obj == null) {
				return false;
			}
			
			return typeName == (obj as Type).typeName;
		}
		
		public override int GetHashCode()
		{
			return typeName.GetHashCode();
		}
	}
}

