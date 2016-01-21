using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using FileHelpers;


public class XPDatabase : Dictionary<Class, Dictionary<int, LevelData>>
{
	public void Load ()
	{
		string filePath = Application.dataPath + "/Resources/Data/XpData/";

		foreach (Class c in Enum.GetValues(typeof(Class))) {

			this.Add (c, new Dictionary<int, LevelData> ());

			string newPath = filePath + c.ToString () + "XP.csv";

			var engine = new FileHelperEngine<LevelData> ();
			var records = engine.ReadFile (newPath);
			
			foreach (var record in records) {
				this [c].Add (record.level, record);
			}
		}
	}
}

// Record Definition
[DelimitedRecord(",")]
[IgnoreFirst]
public class LevelData
{
	public int level;
	public int requiredXP;
	public decimal atkGrowth;
	public decimal hpGrowth;
	public decimal defGrowth;

}
