using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

// Container class for FighterData XML Serialization.
[XmlRoot]
public class FighterDatabase
{

    [XmlElement ("Fighters")]
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


//	public string id = "DEFAULT_ID";

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
    public string
        fighterClass = "Warrior";

    [XmlElement ("Element")]
    public FighterElement
        fighterElement;

    [XmlElement ("Cost")]
    public int
        cost = 2;

    [XmlElement ("ActiveIndex")]
    public int
        activeTroopIndex = -1;

    [XmlIgnore]
    private Sprite[]
        _sprites;
    [XmlIgnore]
    public Sprite[] sprites
    {
        get
        {
            if (_sprites == null || _sprites.Length == 0)
            {
                _sprites = Resources.LoadAll<Sprite>("Sprites/Classes/" + fighterClass);
            }

            return _sprites;
        }
    }

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
