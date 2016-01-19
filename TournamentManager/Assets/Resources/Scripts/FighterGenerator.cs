using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class FighterGenerator {

	public static FighterData GenerateFighter ()
	{
		FighterData newFighter = new FighterData();

		newFighter.id = GUIDGenerator.NewGuid();

		GachaDatabase gachaDatabase = GameDatabase.gachaDatabase;

		newFighter.name = gachaDatabase.GetRandomName ();

		// TODO: Implement readonly interface for data.
		ClassData classData = gachaDatabase.GetRandomClass ();
		newFighter.fighterClass = classData.fighterClass;

		newFighter.ATK = classData.baseATK;
		newFighter.HP = classData.baseHP;
		newFighter.fighterElement = gachaDatabase.GetRandomElement ();


		// TEMP. TODO: Must define isRanged in Data.
		if (newFighter.fighterClass == Class.Warrior) {
			newFighter.isRanged = false;
		} else {
			newFighter.isRanged = true;
		}


		RandomizeEquipment (newFighter);
//		RandomizeSkin (newFighter);

		return newFighter;
	}

	static void RandomizeEquipment (FighterData fighterData)
	{
		ClassData classData = GameDatabase.classDatabase [fighterData.fighterClass];

		foreach (string equipmentType in classData.equipmentAllowed) 
		{
			Debug.Log ("GET EQUIP POOL: " + equipmentType);
			List<Equipment> equipmentPool = GameDatabase.equipmentDatabase [equipmentType].Values.ToList ();

			if (equipmentPool != null) {
//				Equipment.Type.Weapon.Sword = new Equipment.Type.Weapon ();

				Debug.Log ("GET: " + equipmentType.ToString ());

				Equipment randomEquipment = equipmentPool[UnityEngine.Random.Range (0, equipmentPool.Count)];
				fighterData.equipmentData.Add (equipmentType, randomEquipment);

				// Save sprites for this equip in FighterData.
				foreach (EquipmentSprite sprite in randomEquipment.sprites) {
					fighterData.skinData.AddSkin (sprite.attachmentType, sprite.spriteName);
				}
			}
		}
	}
}
