using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;


[XmlRoot]
public class PlayerData
{
	#region Currency
	[XmlElement ("Gold")]
	private int _gold = 1000;
	public int gold {
		get {
			return _gold;
		}
		set {
			_gold = value;
			Save ();
		}
	}
	
	[XmlElement ("Diamonds")]
	private int _diamonds = 500;
	public int diamonds {
		get {
			return _diamonds;
		}
		set {
			_diamonds = value;
			Save ();
		}
	}
	#endregion


    [XmlElement ("TournamentProgress")]
    public int tournamentProgress;

    [XmlElement ("TournamentMatchCount")]
    public int tournamentMatchCount;
 
	// TODO: Party class. User may create party presets.
	[XmlElement ("CurrentParty")]
	public Party currentParty = new Party();
//	public Party currentParty {
//		get {
//			if (_currentParty == null) {
//				_currentParty = new Party ();
//			}
//
//			return _currentParty;
//		}
//	}

	[XmlArray("UnlockedEquipment")]
	[XmlArrayItem("ID")]
	public List<string> unlockedEquipment = new List<string> () { "sword" };

//    [XmlArray("FightersOwned")]
//    [XmlArrayItem("Fighter")]
//    public List<FighterData> fightersOwned = new List<FighterData>();

	public SerializableDictionary<string, FighterData> fightersOwned = new SerializableDictionary<string, FighterData> ();

	
    [XmlElement("PartyCapacity")]
    public int partyCapacity = 20;

    /// <summary>
    /// The fighter base capacity.
    /// Do not access this directly. Use the GetFighterCapacity method on GameData instead
    /// </summary>
    [XmlElement("FighterCapacity")]
    public int fighterCapacity = 50;

    // TOWN
    [XmlElement("Town")]
    public TownData town;
	
    public PlayerData() 
    {
        town = new TownData();
    }

	public void UnlockEquipment (string id)
	{
		if (!unlockedEquipment.Contains (id)) {
			unlockedEquipment.Add (id);
			Save ();
		} else {
			Debug.LogWarning ("Equipment: " + id + " already unlocked.");
		}
	}

	public void AddToParty (FighterData fighter)
	{
		if (currentParty == null) {
			currentParty = new Party ();
		}

		PartyManager.AddToParty (fighter, currentParty);

		Save ();
	}

	public void RemoveFromParty (FighterData fighter)
	{
		PartyManager.RemoveFromParty (fighter, currentParty);

		Save ();
	}

	#region Save/Load
    public void Save()
    {
		Debug.Log ("Saving...");
		XmlHelper.Save<PlayerData> (this, "PlayerData");
    }

    public static PlayerData Load()
    {
		return XmlHelper.Load<PlayerData> ("PlayerData");;
	}
	#endregion
}








