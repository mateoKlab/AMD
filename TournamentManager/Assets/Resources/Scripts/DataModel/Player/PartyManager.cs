using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

public static class PartyManager {

	// TODO: Add functionality for party preset switching.
	public static int maxPartyCapacity;

	public static void AddToParty (FighterData fighter, Party party)
	{
		// TODO: party capacity get.
		if ((party.currentCost + fighter.cost) < GameData.instance.playerData.partyCapacity) {
			party.Add (fighter);
		}
	}

	public static void RemoveFromParty (FighterData fighter, Party party)
	{
		party.Remove (fighter);
	}
}

[XmlRoot]
public class Party {

	private int _currentCost;
	public int currentCost {
		get { 
			return _currentCost;
		}

		private set {
			_currentCost = value;
		}
	}

	[XmlArray("Fighters")]
	[XmlArrayItem("ID")]
	public List<string> fighters;

	public void Add (FighterData fighter)
	{
		Debug.Log ("ADDING...");
		if (!fighters.Contains (fighter.id)) {
			fighters.Add (fighter.id);

			currentCost += fighter.cost;
			Debug.Log ("ADDED...");

		}	
	}
	
	public void Remove (FighterData fighter)
	{
		if (fighters.Contains (fighter.id)) {
			fighters.Remove (fighter.id);

			currentCost -= fighter.cost;
		}
	}
}
