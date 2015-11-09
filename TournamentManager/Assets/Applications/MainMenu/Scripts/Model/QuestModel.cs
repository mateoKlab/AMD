using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestModel : Model
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public List<StageData> questList = new List<StageData>();
	public List<Transform> questPoints = new List<Transform>();

	public override void Awake() 
	{
		base.Awake();
		PopulateQuestList();
	}
	
	private void PopulateQuestList()
	{
		// HACK Test code
		Debug.Log ("PopulateQuestList");
		
		foreach (Transform qPoint in GetComponentsInChildren<Transform>())
		{
			if (qPoint != transform)
				questPoints.Add(qPoint);
		}
		
		StageData testData = new StageData();
		
		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 2000;
		testFighter1.ATK = 50;
		
		testData.enemies.Add (testFighter1);
		questList.Add(testData);
		
		testData.enemies.Add (testFighter1);
		questList.Add(testData);
		
	}
}
