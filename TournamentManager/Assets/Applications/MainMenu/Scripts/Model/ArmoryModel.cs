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

		SerializableDictionary<Equipment.EquipmentType, List<Equipment>> warriorEquips = GameDatabase.equipmentDatabase [FighterClass.Warrior];
		SerializableDictionary<Equipment.EquipmentType, List<Equipment>> mageEquips = GameDatabase.equipmentDatabase [FighterClass.Mage];
		SerializableDictionary<Equipment.EquipmentType, List<Equipment>> archerEquips = GameDatabase.equipmentDatabase [FighterClass.Archer];
	

		for (int i = 0; i < warriorEquips[Equipment.EquipmentType.Weapon].Count; i++)
		{
			weaponList.Add(warriorEquips[Equipment.EquipmentType.Weapon][i]);
		}
		for (int i = 0; i < mageEquips[Equipment.EquipmentType.Weapon].Count; i++)
		{
			weaponList.Add(mageEquips[Equipment.EquipmentType.Weapon][i]);
		}
		for (int i = 0; i < archerEquips[Equipment.EquipmentType.Weapon].Count; i++)
		{
			weaponList.Add(archerEquips[Equipment.EquipmentType.Weapon][i]);
		}
	

		for (int i = 0; i < warriorEquips[Equipment.EquipmentType.Body].Count; i++)
		{
			armorList.Add(warriorEquips[Equipment.EquipmentType.Body][i]);
		}
		for (int i = 0; i < mageEquips[Equipment.EquipmentType.Body].Count; i++)
		{
			armorList.Add(mageEquips[Equipment.EquipmentType.Body][i]);
		}
		for (int i = 0; i < archerEquips[Equipment.EquipmentType.Body].Count; i++)
		{
			armorList.Add(archerEquips[Equipment.EquipmentType.Body][i]);
		}

	}
}
