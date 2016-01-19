using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;


public class XPDatabase : Dictionary<Class, Dictionary<int, int>>

{

	public void Load ()
	{
		string filePath = Application.dataPath + "/Resources/Data/";

		foreach (Class c in Enum.GetValues(typeof(Class))) {

			this.Add (c, new Dictionary<int, int> ());

			string newPath = filePath + c.ToString () + "XP.csv";

			var engine = new FileHelperEngine<XPrecord> ();
			var records = engine.ReadFile (newPath);
			
			foreach (var record in records) {
				this [c].Add (record.level, record.requiredXP);
			}
		}
	}


}

// Record Definition
[DelimitedRecord(",")]
public class XPrecord
{
	public int level;
	public int requiredXP;

}
