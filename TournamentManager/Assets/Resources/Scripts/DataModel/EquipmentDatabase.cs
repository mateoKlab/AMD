using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot]
public class EquipmentDatabase : Database<Equipment>
{
	[XmlArray ("EquipmentList")]
	[XmlArrayItem ("Equipment")]
	public List<Equipment> equipmentList;

	public void Load ()
	{
		// Build EquipmentDatabase from xml list.
		foreach (Equipment equipment in equipmentList) {

			Type equipmentType = Type.GetType (equipment.type);
			this.AddItem (equipmentType, equipment);

		}
	}
}