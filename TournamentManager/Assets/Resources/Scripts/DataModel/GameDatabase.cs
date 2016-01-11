using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
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
	private static Database<Equipment> _equipmentDatabase;

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

	public static Database<Equipment> equipmentDatabase 
	{
		get {
			if (_equipmentDatabase == null) {
				_equipmentDatabase = LoadDatabase<Database<Equipment>> ();
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
		_spriteDatabase    = LoadDatabase<SpriteDatabase> ();
		_stageDatabase	   = LoadDatabase<StageDatabase> ();
		_equipmentDatabase = LoadDatabase<Database<Equipment>> ();
		_classDatabase 	   = LoadDatabase<ClassDatabase> ();

		List<Equipment> testList = _equipmentDatabase.GetItems (Equipment.Type.Weapon.Sword, false);
		Debug.Log (testList [0].spriteName);
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

// List of Stages for each StageType.
[XmlRoot ("StageDatabase")]
public class StageDatabase : SerializableDictionary<StageType, 
							 SerializableDictionary<string, StageData>>
{ }

[XmlRoot ("ClassDatabase")]
public class ClassDatabase : SerializableDictionary<Class, ClassData>
{ }