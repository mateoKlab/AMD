using UnityEngine;

using UnityEditor;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public static class GameDatabase {

	// List of sprites(string filename) available for each sprite attachment/body part.
	private static SpriteDatabase _spriteDatabase;

	// List of StageData available for each type.
	private static StageDatabase _stageDatabase;

	// List of Equipment available for each type.
	private static EquipmentDatabase _equipmentDatabase;

	private static GachaDatabase _gachaDatabase;

	private static ClassDatabase _classDatabase;

	#region Getters
	public static SpriteDatabase spriteDatabase 
	{
		get {
			if (_spriteDatabase == null) {
				_spriteDatabase = LoadDatabase<SpriteDatabase> ();
			}

			return _spriteDatabase; 
		}
	}

	public static StageDatabase stageDatabase 
	{
		get {
			if (_stageDatabase == null) {
				_stageDatabase = LoadDatabase<StageDatabase> ();
			}
			
			return _stageDatabase; 
		}
	}

	public static EquipmentDatabase equipmentDatabase 
	{
		get {
			if (_equipmentDatabase == null) {
				_equipmentDatabase = LoadDatabase<EquipmentDatabase> ();
			}
			
			return _equipmentDatabase; 
		}
	}

	public static GachaDatabase gachaDatabase 
	{
		get {
			if (_gachaDatabase == null) {
				_gachaDatabase = LoadDatabase<GachaDatabase> ();
			}
			
			return _gachaDatabase; 
		}
	}

	public static ClassDatabase classDatabase 
	{
		get {
			if (_classDatabase == null) {
				_classDatabase = LoadDatabase<ClassDatabase> ();
			}
			
			return _classDatabase; 
		}
	}
	#endregion

	public static void LoadGameDatabase ()
	{
		// Initialize here.
		_spriteDatabase = LoadDatabase<SpriteDatabase> ();
		_stageDatabase  = LoadDatabase<StageDatabase> ();
		_equipmentDatabase = LoadDatabase<EquipmentDatabase> ();
//		_classDatabase = LoadDatabase<ClassDatabase> ();

		ClassDatabase testData = new ClassDatabase ();

		ClassData warrior = new ClassData ();
		warrior.fighterClass = Class.Warrior;
		warrior.baseATK = 100;
		warrior.baseHP = 1000;
		warrior.baseDEF = 10;

		ClassData archer = new ClassData ();
		archer.fighterClass = Class.Archer;
		archer.baseATK = 75;
		archer.baseHP = 800;
		archer.baseDEF = 0;


		ClassData mage = new ClassData ();
		mage.fighterClass = Class.Mage;
		mage.baseATK = 150;
		mage.baseHP = 500;
		mage.baseDEF = 0;


		testData.Add (warrior.fighterClass, warrior);
		testData.Add (archer.fighterClass, archer);
		testData.Add (mage.fighterClass, mage);

		
		List<Equipment.Type> types = new List<Equipment.Type> ();
		types.Add (Equipment.Type.Weapon.Sword);
		types.Add (Equipment.Type.Body.HeavyArmor);

		warrior.equipmentAllowed = types;
		XmlSerializer xmls = new XmlSerializer(typeof(ClassDatabase));
		
		#if UNITY_EDITOR || UNITY_IOS
		using(StringWriter sww = new StringWriter())
			using(XmlWriter writer = XmlWriter.Create(sww))
		{
			xmls.Serialize(writer, testData);
			
			// Using XmlDocument guarantees a properly formatted xml file.
			XmlDocument xdoc = new XmlDocument();
			xdoc.LoadXml(sww.ToString());
			xdoc.Save(Application.dataPath + "/Resources/Data/ClassDatabase.xml");
		}
		
		#elif UNITY_ANDROID
		string filePath = GetPath("PlayerData.xml");
		using (var stream = System.IO.File.CreateText(filePath))
		{
			xmls.Serialize(stream, this);
			stream.Close();
		}
		#endif
		
		#if UNITY_EDITOR
		AssetDatabase.Refresh();
		#endif
		
	}
	
	private static T LoadDatabase<T> ()
	{
		XmlSerializer ser = new XmlSerializer(typeof (T));
		
		TextAsset textAsset = Resources.Load("Data/" + typeof (T).ToString ()) as TextAsset;
		System.IO.StringReader stringReader;
		
		if (textAsset == null)
		{
			Debug.LogError (typeof (T).ToString() + " not found!");
			return default(T);
		}
		else
		{
			stringReader = new System.IO.StringReader(textAsset.text);
			using (XmlReader reader = XmlReader.Create(stringReader))
			{
				return (T)ser.Deserialize(reader);
			}
		}
	}
}


// Container classes for XML Serialization.
// TODO: Implement Read-only wrapper class for SerializableDictionary.

// List of sprites available for each class.
[XmlRoot ("SpriteDatabase")]
public class SpriteDatabase : SerializableDictionary<Class, 
SerializableDictionary <FighterSpriteAttachment.AttachmentType, List <string>>>
{ }

// List of equipment available for each class.
[XmlRoot ("EquipmentDatabase")]
public class EquipmentDatabase : SerializableDictionary<Equipment.Type, List<Equipment>>
{ }

// List of Stages for each StageType.
[XmlRoot ("StageDatabase")]
public class StageDatabase : SerializableDictionary<StageType, SerializableDictionary<string, StageData>>
{ }

[XmlRoot ("ClassDatabase")]
public class ClassDatabase : SerializableDictionary<Class, ClassData>
{ }