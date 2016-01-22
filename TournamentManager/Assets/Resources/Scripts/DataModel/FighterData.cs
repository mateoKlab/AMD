using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// Container class for FighterData XML Serialization.
[XmlRoot]
public class FighterDatabase
{
   [XmlArray("Fighters")]
   [XmlArrayItem("Fighter")]
    public List<FighterData>
        fighters;
}

public enum FighterElement
{
    Fire,
    Water,
    Lightning,
    Earth
}

public enum FighterAttackType
{
    Melee,
    Ranged
}

[XmlRoot]
public class FighterData
{
    [XmlElement]
    public string id = "";

    //TEMPORARY. default to knight.
    [XmlElement ("SpriteName")]
    public string spriteName = "knight_fire";

    [XmlElement]
    public int HP = 1000;

    [XmlElement ("MaxHP")]
    public int maxHP = 1000;

    [XmlElement]
    public int ATK = 100;

	public int DEF = 0;

    [XmlElement]
    public bool isRanged = false;

	[XmlElement ("Level")]
	public int level = 1;

	// TODO: Revise XP system, add totalexp, currentexp.
	[XmlElement ("XP")]
	public int exp = 0;

    [XmlElement ("Name")]
    public string name = "Juan";

    [XmlElement ("Class")]
	public Class fighterClass = Class.Warrior;

    [XmlElement ("Element")]
    public FighterElement fighterElement;

    [XmlElement ("Cost")]
    public int cost = 2;
	
	[XmlElement ("EquipmentData")]
	public SerializableDictionary<string, Equipment> equipmentData = new SerializableDictionary<string, Equipment> ();

	[XmlElement ("SkinData")]
	public FighterSkinData skinData = new FighterSkinData ();



}
