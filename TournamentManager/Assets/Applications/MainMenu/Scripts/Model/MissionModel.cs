using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

public class MissionModel : Model
{
    
	public Dictionary<string, StageData> missionDictionary;
	public List<StageData> missionPointDataList = new List<StageData>();
	public List<MissionPointModel> missionPointModelsList = new List<MissionPointModel>(); 

	public override void Awake() 
	{
		base.Awake();
		PopulateMissionList();
	}
	
	private void PopulateMissionList()
	{

		StageDatabase stageDatabaseClone = GameDatabase.stageDatabase;

		missionDictionary = stageDatabaseClone[StageType.Mission];
		
		foreach (StageData mData in missionDictionary.Values)
		{
			missionPointDataList.Add(mData);
		}

		foreach (MissionPointModel mPointModel in transform.root.GetComponentsInChildren<MissionPointModel>()) {
			missionPointModelsList.Add(mPointModel);
		}

		for(int i = 0; i < missionPointModelsList.Count; i++) {
			missionPointModelsList[i].SetStageData(missionPointDataList[i]);
		}
	}
	
}
