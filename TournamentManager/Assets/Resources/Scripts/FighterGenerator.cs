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
		newFighter.maxHP = classData.baseHP;
		newFighter.fighterElement = gachaDatabase.GetRandomElement ();

		RandomizeEquipment (newFighter);

		return newFighter;
	}

	static void RandomizeEquipment (FighterData fighterData)
	{
		ClassData classData = GameDatabase.classDatabase [fighterData.fighterClass];

		foreach (string equipmentType in classData.equipmentAllowed) 
		{
			List<Equipment> equipmentPool = GameDatabase.equipmentDatabase.GetItems (Type.GetType(equipmentType));

			if (equipmentPool.Count > 0) {

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
