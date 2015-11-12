using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionModel : Model
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public Dictionary<string, StageData> missionDictionary;
	public List<Transform> missionPoints = new List<Transform>();

	public override void Awake() 
	{
		base.Awake();
		PopulateMissionList();
	}
	
	private void PopulateMissionList()
	{

		Dictionary<StageType, Dictionary<string, StageData>> stageDatabaseClone = GameData.instance.stageDatabase;
		missionDictionary = stageDatabaseClone[StageType.Mission];
		
		foreach (Transform mPoint in GetComponentsInChildren<Transform>())
		{
			if (mPoint != transform)
				missionPoints.Add(mPoint);
		}
		
	}
}
