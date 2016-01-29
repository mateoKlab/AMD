using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class XPDatabase : Dictionary<Class, Dictionary<int, LevelData>>
{
	public void Load ()
	{
		Dictionary<int, LevelData> levelDic = new Dictionary<int, LevelData> ();
		LevelData ld1 = new LevelData () { level = 1, requiredXP = 0, atkGrowth = 1.0f, hpGrowth = 1.0f, defGrowth = 1.0f };
		LevelData ld2 = new LevelData () { level = 2, requiredXP = 1000, atkGrowth = 1.1f, hpGrowth = 1.1f, defGrowth = 1.1f };
		LevelData ld3 = new LevelData () { level = 3, requiredXP = 2000, atkGrowth = 1.2f, hpGrowth = 1.2f, defGrowth = 1.2f };
		LevelData ld4 = new LevelData () { level = 4, requiredXP = 3000, atkGrowth = 1.3f, hpGrowth = 1.3f, defGrowth = 1.3f };
		LevelData ld5 = new LevelData () { level = 5, requiredXP = 4000, atkGrowth = 1.4f, hpGrowth = 1.4f, defGrowth = 1.4f };
		LevelData ld6 = new LevelData () { level = 6, requiredXP = 5000, atkGrowth = 1.5f, hpGrowth = 1.5f, defGrowth = 1.5f };
		LevelData ld7 = new LevelData () { level = 7, requiredXP = 6000, atkGrowth = 1.6f, hpGrowth = 1.6f, defGrowth = 1.6f };
		LevelData ld8 = new LevelData () { level = 8, requiredXP = 7000, atkGrowth = 1.7f, hpGrowth = 1.7f, defGrowth = 1.7f };
		LevelData ld9 = new LevelData () { level = 9, requiredXP = 8000, atkGrowth = 1.8f, hpGrowth = 1.8f, defGrowth = 1.8f };
		LevelData ld10 = new LevelData () { level = 10, requiredXP = 9000, atkGrowth = 1.9f, hpGrowth = 1.9f, defGrowth = 1.9f };

		levelDic.Add (1, ld1);
		levelDic.Add (2, ld2);
		levelDic.Add (3, ld3);
		levelDic.Add (4, ld4);
		levelDic.Add (5, ld5);
		levelDic.Add (6, ld6);
		levelDic.Add (7, ld7);
		levelDic.Add (8, ld8);
		levelDic.Add (9, ld9);
		levelDic.Add (10, ld10);

		this.Add (Class.Warrior, levelDic);
		this.Add (Class.Mage, levelDic);

//		string filePath = Application.dataPath + "/Resources/Data/XpData/";
//
//		foreach (Class c in Enum.GetValues(typeof(Class))) {
//
//			this.Add (c, new Dictionary<int, LevelData> ());
//
//			string newPath = filePath + c.ToString () + "XP.csv";
//
//			/////
//			var engine = new FileHelperEngine<LevelData> ();
//			var records = engine.ReadFile (newPath);
//			
//			foreach (var record in records) {
//				this [c].Add (record.level, record);
//			}
		}
}

public class LevelData
{
	public int level;
	public int requiredXP;
	public float atkGrowth; //decimal atkGrowth;
	public float hpGrowth; // decimal hpGrowth;
	public float defGrowth; // decimal defGrowth;

}
