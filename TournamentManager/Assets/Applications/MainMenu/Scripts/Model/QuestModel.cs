using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

public class QuestModel : Model
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public Dictionary<string, StageData> questDictionary;
	public List<Transform> questPoints = new List<Transform>();

	public override void Awake() 
	{
		base.Awake();
		PopulateQuestList();
	}
	
	private void PopulateQuestList()
	{

		Dictionary<StageType, Dictionary<string, StageData>> stageDatabaseClone = GameData.instance.stageDatabase;
		questDictionary = stageDatabaseClone[StageType.Quest];
		
		foreach (Transform qPoint in GetComponentsInChildren<Transform>())
		{
			if (qPoint != transform)
				questPoints.Add(qPoint);
		}
		
	}
}
