using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public static class FighterGenerator {

	public static FighterData GenerateFighter ()
	{
		// TEMPORARY. TODO: Load this from file, and cache it.
		List<string> namePool = new List<string> { "AJ", "Dave", "Matt", "Rence", "JC", "Ryan", "Khai", "Jeff", "Jed" };
		List<FighterClass> classPool = Enum.GetValues (typeof(FighterClass)).Cast <FighterClass> ().ToList ();

		FighterData newFighter = new FighterData();

		newFighter.id = GUIDGenerator.NewGuid();
		newFighter.name = namePool [UnityEngine.Random.Range (0, namePool.Count)];

		// TEMPORARY. Set stats. TODO: Get stat ranges, names, etc from a GachaDatabase/pool.
		newFighter.fighterElement = (FighterElement)UnityEngine.Random.Range(0, 4);
		newFighter.HP  = (int)UnityEngine.Random.Range(5, 15) * 100; 
		newFighter.ATK = (int)UnityEngine.Random.Range(5, 15) * 10;

		// TEST. Assign Melee.
		newFighter.isRanged = false;
//		newFighter.isRanged = System.Convert.ToBoolean (UnityEngine.Random.Range (0, 2)); // Random between 0 and 1. Max value is exclusive.

		// TEMPORARY. Assign Class. TODO: Assign Class from GachaDatabase/pool (Not all classes will be available through gacha).
//		newFighter.fighterClass = classPool[UnityEngine.Random.Range (0, classPool.Count)];
		newFighter.fighterClass = FighterClass.Warrior;

		RandomizeEquipment (newFighter);
		RandomizeSkin (newFighter);

		return newFighter;
	}

	// TODO: EquipmentGenerator.
	static void RandomizeEquipment (FighterData fighterData)
	{
		SerializableDictionary<Equipment.EquipmentType, List<Equipment>> classEquips = GameDatabase.equipmentDatabase [fighterData.fighterClass];

		foreach (Equipment.EquipmentType type in classEquips.Keys) {
			int randomWeapon = UnityEngine.Random.Range (0, classEquips[type].Count);

			Equipment newEquip = classEquips [type][randomWeapon];

			fighterData.equipmentData.Add (type, newEquip);

			FighterSpriteAttachment.AttachmentType attachmentType = (FighterSpriteAttachment.AttachmentType) Enum.Parse(typeof(FighterSpriteAttachment.AttachmentType), newEquip.type.ToString ());
			fighterData.skinData.Add (attachmentType, newEquip.spriteName);
			Debug.Log ("EQUIP: " + newEquip.spriteName);
		}
	}


	// TODO: Change to "RandomizeEquipment" ().
	// TODO: Add chance to  accesssories/cape/wing. 
	public static void RandomizeSkin (FighterData fighterData)
	{
		// List of possible sprites for this class.
		SerializableDictionary<FighterSpriteAttachment.AttachmentType, List<string>> spritePool = GameDatabase.spriteDatabase [fighterData.fighterClass];

		foreach (FighterSpriteAttachment.AttachmentType attachment in spritePool.Keys) {
			List<string> attachmentPool = spritePool[attachment];

			int randomAttachment = UnityEngine.Random.Range (0, attachmentPool.Count);

			fighterData.skinData.Add (attachment, attachmentPool[randomAttachment]);
		}
	}
}
