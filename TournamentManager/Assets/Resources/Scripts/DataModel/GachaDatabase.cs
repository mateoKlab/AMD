using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[XmlRoot]
public class GachaDatabase {

	public List<string> namePool;
	public List<FighterElement> elementPool;

	// List of classes that can be obtained through gacha.
	public List<Class> classPool;

	public string GetRandomName ()
	{
		return namePool [UnityEngine.Random.Range (0, namePool.Count)];
	}

	// TODO: Implement readonly interface for data.
	public ClassData GetRandomClass ()
	{
//		Class randomClass = classPool [UnityEngine.Random.Range (0, classPool.Count)];
		Class randomClass;
		int random = UnityEngine.Random.Range (0, 11);
		if (random > 5) {
			randomClass = Class.Warrior;
		} else {
			randomClass = Class.Mage;
		}

		// TEST.
		randomClass = Class.Warrior;
	
		return GameDatabase.classDatabase [randomClass];
	}

	public FighterElement GetRandomElement ()
	{
		return elementPool [UnityEngine.Random.Range (0, elementPool.Count)];
	}

//	public int GetClassBaseAttack (FighterClass fighterClass)
//	{
//		return classPool [fighterClass].attack;
//	}
//
//	public int GetClassBaseHP (FighterClass fighterClass)
//	{
//		return classPool [fighterClass].hp;
//	}


	public struct BaseStats {
		public int hp;
		public int attack;
	}
}

