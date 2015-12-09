using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class ArmoryModel : Model
{
	public List<Equipment> weaponList = new List<Equipment>();
	public List<Equipment> armorList = new List<Equipment>();


	// The following are test variables only
	public List<Equipment> unlockedItems = new List<Equipment>();

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
	
		// Test
		for (int i = 0; i < weaponList.Count/2; i++)
		{
			unlockedItems.Add(weaponList[i]);
		}

		for (int i = 0; i < armorList.Count/2; i++)
		{
			unlockedItems.Add(armorList[i]);
		}

	}
}
