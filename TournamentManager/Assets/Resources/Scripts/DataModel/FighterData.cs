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

    [XmlElement ("Level")]
    public int level = 1;

    [XmlElement]
    public int HP = 1000;

    [XmlElement ("MaxHP")]
    public int maxHP = 1000;

    [XmlElement]
    public int ATK = 100;

    [XmlElement]
    public bool isRanged = false;

    [XmlElement ("Name")]
    public string name = "Juan";

    [XmlElement ("Class")]
	public Class fighterClass = Class.Warrior;

    [XmlElement ("Element")]
    public FighterElement fighterElement;

    [XmlElement ("Cost")]
    public int cost = 2;

	[XmlElement ("XP")]
	public int xp = 0;

	[XmlElement ("EquipmentData")]
//	public SerializableDictionary<Equipment.Type, Equipment> equipmentData = new SerializableDictionary<Equipment.Type, Equipment> ();
	public SerializableDictionary<string, Equipment> equipmentData = new SerializableDictionary<string, Equipment> ();

	[XmlElement ("SkinData")]
	public FighterSkinData skinData = new FighterSkinData ();

    [XmlIgnore]
    public Sprite normalIcon
    {
        get
        {
            //return sprites[0];
            return Resources.Load<Sprite>("Sprites/" + spriteName);
        }
    }
}
