using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionView : View
{
	public void OnClickMissionPoint(string stageID) {
		((MissionController)controller).GoToMission(((MissionModel)model).missionDictionary[stageID]);
	}
}
