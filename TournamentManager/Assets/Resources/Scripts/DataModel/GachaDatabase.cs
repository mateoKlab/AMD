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

	public SerializableDictionary<FighterClass, BaseStats> classPool;

	public string GetRandomName ()
	{
		return namePool [UnityEngine.Random.Range (0, namePool.Count)];
	}

	public FighterClass GetRandomClass ()
	{
		List<FighterClass> classes = new List<FighterClass>(classPool.Keys);
		return classes [UnityEngine.Random.Range (0, classes.Count)];
	}

	public FighterElement GetRandomElement ()
	{
		return elementPool [UnityEngine.Random.Range (0, elementPool.Count)];
	}

	public int GetClassBaseAttack (FighterClass fighterClass)
	{
		return classPool [fighterClass].attack;
	}

	public int GetClassBaseHP (FighterClass fighterClass)
	{
		return classPool [fighterClass].hp;
	}


	public struct BaseStats {
		public int hp;
		public int attack;
	}
}

