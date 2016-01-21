﻿using UnityEngine;
using System.Collections;

public class LevelUpController
{
	private XPDatabase xpDatabase;
	private ClassDatabase classDatabase;

	public LevelUpController (XPDatabase xpDatabase, ClassDatabase classDatabase) 
	{
		this.xpDatabase = xpDatabase;
		this.classDatabase = classDatabase;
	}


	public bool CheckLevelUp (FighterData fighterData)
	{
		int currentLevel = fighterData.level;
		int currentExp   = fighterData.exp;

		// IF: Already at max level.
		if (currentLevel >= xpDatabase [Class.Warrior].Keys.Count) {
			return false;
		}

		int expRequired = xpDatabase [Class.Warrior][currentLevel + 1].requiredXP;
		if (currentExp >= expRequired) {
			return true;
		} else {
			return false;
		}

	}
	
	public void LevelUp (FighterData fighterData)
	{
		fighterData.level += 1;

		LevelData levelData = xpDatabase [fighterData.fighterClass] [fighterData.level];
		ClassData classData = classDatabase [fighterData.fighterClass];

		fighterData.HP  += (int)(levelData.hpGrowth * classData.baseHP);
		fighterData.ATK += (int)(levelData.atkGrowth * classData.baseATK);
		fighterData.DEF += (int)(levelData.defGrowth * classData.baseDEF);
	}
			

}
