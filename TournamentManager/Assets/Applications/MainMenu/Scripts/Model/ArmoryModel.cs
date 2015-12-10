using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class ArmoryModel : Model
{
	public List<Equipment> weaponList = new List<Equipment>();
	public List<Equipment> armorList = new List<Equipment>();

	//Test

	public override void Awake ()
	{
		base.Awake ();
	
		weaponList = EquipmentDatabase.GetEquipment(Equipment.Type.Weapon.Sword, false);
		armorList = EquipmentDatabase.GetEquipment(Equipment.Type.Body.HeavyArmor, false);
	}
}
