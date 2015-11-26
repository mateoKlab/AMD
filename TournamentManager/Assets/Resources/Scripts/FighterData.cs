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

[XmlRoot ("Class")]
public enum FighterClass
{
	Warrior,
	Mage,
	Archer
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
    public string
        spriteName = "knight_fire";

    [XmlElement]
    public int
        HP = 1000;

    [XmlElement ("MaxHP")]
    public int
        maxHP = 1000;

    [XmlElement]
    public int
        ATK = 100;

    [XmlElement]
    public bool
        isRanged = false;

    [XmlElement ("Name")]
    public string
        name = "Juan";

    [XmlElement ("Class")]
	public FighterClass
        fighterClass = FighterClass.Warrior;

    [XmlElement ("Element")]
    public FighterElement
        fighterElement;

    [XmlElement ("Cost")]
    public int
        cost = 2;

	// TODO: Put initializations into constructor.
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
