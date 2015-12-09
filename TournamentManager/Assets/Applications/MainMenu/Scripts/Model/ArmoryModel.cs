using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class ArmoryModel : Model
{
	public List<Equipment> weaponList = new List<Equipment>();
	public List<Equipment> armorList = new List<Equipment>();

	//Test
	public List<Equipment> unlockedItems = new List<Equipment>();

	public override void Awake ()
	{
		base.Awake ();
	
		weaponList = EquipmentDatabase.GetEquipment(Equipment.Type.Weapon.Sword, false);
		armorList = EquipmentDatabase.GetEquipment(Equipment.Type.Body.HeavyArmor, false);


		// Test
		for (int i = 0; i < weaponList.Count/2; i++) {
			unlockedItems.Add(weaponList[i]);
		}
		for (int i = 0; i < armorList.Count/2; i++) {
			unlockedItems.Add(armorList[i]);
		}
	}
}
