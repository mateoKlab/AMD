using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class ArmoryModel : Model
{
	public List<Equipment> weaponList = new List<Equipment>();
	public List<Equipment> armorList = new List<Equipment>();

	public override void Awake ()
	{
		base.Awake ();
	
		foreach (Equipment.Type type in GameDatabase.equipmentDatabase.Keys) {
			//	Examples:
			//		if (type is Equipment.Type.Weapon) {}
			//		if (type.typeName == Equipment.Type.Weapon.Sword) {}  

			if (type.typeName == "Sword") {
				weaponList = GameDatabase.equipmentDatabase[type];
			}

			if (type.typeName == "HeavyArmor") {
				armorList = GameDatabase.equipmentDatabase[type];
			}
		}
	

	}
}
