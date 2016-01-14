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
		Debug.Log ("WOW");
		// Initialize here.
		_spriteDatabase    = LoadDatabase<SpriteDatabase> ();
		_stageDatabase	   = LoadDatabase<StageDatabase> ();
		_equipmentDatabase = LoadDatabase<EquipmentDatabase> ();
		_classDatabase 	   = LoadDatabase<ClassDatabase> ();


//		Equipment sword = new Equipment ();
//		sword.attack = 10;
//		sword.defense = 0;
//		sword.hp = 0;
//		sword.id = "Sword";
//		sword.name = "cool sword";
//		sword.sprites = new List<EquipmentSprite> ();
//		sword.sprites.Add (new EquipmentSprite { attachmentType = "Sword", spriteName = "Sword" });
//
//		Equipment helm = new Equipment ();
//		helm.attack = 0;
//		helm.defense = 10;
//		helm.hp = 100;
//		helm.id = "Helm";
//		helm.name = "cool helm";
//		helm.sprites = new List<EquipmentSprite> ();
//		helm.sprites.Add (new EquipmentSprite { attachmentType = "Helm", spriteName = "Helm" });
//
//		SerializableDictionary<string, Equipment> swordDic = new SerializableDictionary<string, Equipment> ();
//		swordDic.Add (sword.id, sword);
//
//		SerializableDictionary<string, Equipment> helmDic = new SerializableDictionary<string, Equipment> ();
//		helmDic.Add (helm.id, helm);
//
//		_equipmentDatabase.Add ("Sword", swordDic);
//		_equipmentDatabase.Add ("Helm", helmDic);
//
//		XmlHelper.Save<EquipmentDatabase> (_equipmentDatabase, "EquipmentDatabase");
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