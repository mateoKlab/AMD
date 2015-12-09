using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot ("EquipmentDatabase")]
public class EquipmentDatabase
{ 
	public SerializableDictionary<Equipment.Type, EquipmentDatabase> databaseBranch;

	// key: Equipment ID value: Equipment
	public SerializableDictionary<string, Equipment> equipmentDictionary;


	public static List<Equipment> GetEquipment (Equipment.Type type, bool recursive) 
	{
		List<Equipment> equipmentList = null;

		EquipmentDatabase database = GetDatabase (type);

		if (database != null) {
			equipmentList = GetEquipment (database, recursive);
		}

		return equipmentList;
	}

	// TODO: GetEquipment from string: equipmentID.


	#region Helper Methods
	// Get EquipmentDatabase for Equipment.Type.
	private static EquipmentDatabase GetDatabase (Equipment.Type type) 
	{
		EquipmentDatabase currentDatabase = GameDatabase.equipmentDatabase;
		Stack<Equipment.Type> typeStack = GetTypeStack (type);

		while (typeStack.Count > 0) {
			Equipment.Type equipmentType = typeStack.Pop ();

			if (currentDatabase.databaseBranch.ContainsKey (equipmentType)) {
				currentDatabase = currentDatabase.databaseBranch [equipmentType];
			} else {
				Debug.LogError ("EquipmentDatabase: " + equipmentType.typeName + " does not exist in database.");
				return null;
			}
		}
		
		return currentDatabase;
	}

	// Get Equipment List from EquipmentDatabase. If recursive = true, also search in nested databases.
	private static List<Equipment> GetEquipment (EquipmentDatabase database, bool recursive)
	{
		List<Equipment> equipmentList = new List<Equipment> ();

		if (database.equipmentDictionary != null) {
			equipmentList.AddRange (database.equipmentDictionary.Values.ToList ());
		}

		// Recursively search through nested databases.
		if (recursive && database.databaseBranch != null) {

			foreach (EquipmentDatabase nestedDatabase in database.databaseBranch.Values) {
				equipmentList.AddRange (GetEquipment (nestedDatabase, recursive));
			}
		}

		return equipmentList;
	}
	
	private static Stack<Equipment.Type> GetTypeStack (Equipment.Type type)
	{
		Equipment.Type currentType = type;

		Stack<Equipment.Type> typeStack = new Stack<Equipment.Type> ();
		typeStack.Push (currentType);

		while (currentType.parentType != null) {

			typeStack.Push (currentType.parentType);

			currentType = type.parentType;
		}
		
		return typeStack;
	}
	#endregion
}

