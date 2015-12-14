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

		foreach (Equipment.Type equipmentType in classData.equipmentAllowed) 
		{
			// TODO: Equip gacha categories. Required/Not required e.g. Sword/armor vs accessories/wings/cape.
			// TODO: Equip drop rate.

			List<Equipment> equipmentPool = GameDatabase.equipmentDatabase.GetItems (equipmentType, false);

			if (equipmentPool != null) {
				Equipment randomEquipment = equipmentPool[UnityEngine.Random.Range (0, equipmentPool.Count)];
				fighterData.equipmentData.Add (equipmentType, randomEquipment);

				// TEMP. add attachment type member to Equipment?
				FighterSpriteAttachment.AttachmentType attachmentType = (FighterSpriteAttachment.AttachmentType) Enum.Parse(typeof(FighterSpriteAttachment.AttachmentType), equipmentType.GetType ().Name); //randomEquipment.type.typeName);
				fighterData.skinData.Add (attachmentType, randomEquipment.spriteName);
			}
		}
	}


	// TODO: Change to "RandomizeEquipment" ().
	// TODO: Add chance to  accesssories/cape/wing. 
	public static void RandomizeSkin (FighterData fighterData)
	{
		// If no sprites for this class exist, return.
		if (!GameDatabase.spriteDatabase.ContainsKey (fighterData.fighterClass)) {
			return;
		}

		// List of possible sprites for this class.
		SerializableDictionary<FighterSpriteAttachment.AttachmentType, List<string>> spritePool = GameDatabase.spriteDatabase [fighterData.fighterClass];

		foreach (FighterSpriteAttachment.AttachmentType attachment in spritePool.Keys) {
			if (!spritePool.ContainsKey (attachment)) {
				continue;
			}

			List<string> attachmentPool = spritePool[attachment];

			int randomAttachment = UnityEngine.Random.Range (0, attachmentPool.Count);

			fighterData.skinData.Add (attachment, attachmentPool[randomAttachment]);
		}
	}
}
