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
		newFighter.fighterClass = gachaDatabase.GetRandomClass ();
		newFighter.ATK = gachaDatabase.GetClassBaseAttack (newFighter.fighterClass);
		newFighter.HP = gachaDatabase.GetClassBaseHP (newFighter.fighterClass);
		newFighter.fighterElement = gachaDatabase.GetRandomElement ();

		// TEMP. TODO: Must define in Data.
		if (newFighter.fighterClass == FighterClass.Warrior) {
			newFighter.isRanged = false;
		} else {
			newFighter.isRanged = true;
		}

		RandomizeEquipment (newFighter);
		RandomizeSkin (newFighter);

		return newFighter;
	}

	// TODO: EquipmentGenerator.
	static void RandomizeEquipment (FighterData fighterData)
	{
		// If no equipment for this class exists, return.
		if (!GameDatabase.equipmentDatabase.ContainsKey (fighterData.fighterClass)) {
			return;
		}

		SerializableDictionary<Equipment.EquipmentType, List<Equipment>> classEquips = GameDatabase.equipmentDatabase [fighterData.fighterClass];

		foreach (Equipment.EquipmentType type in classEquips.Keys) {
			int randomWeapon = UnityEngine.Random.Range (0, classEquips[type].Count);

			Equipment newEquip = classEquips [type][randomWeapon];

			fighterData.equipmentData.Add (type, newEquip);

			FighterSpriteAttachment.AttachmentType attachmentType = (FighterSpriteAttachment.AttachmentType) Enum.Parse(typeof(FighterSpriteAttachment.AttachmentType), newEquip.type.ToString ());
			fighterData.skinData.Add (attachmentType, newEquip.spriteName);
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
