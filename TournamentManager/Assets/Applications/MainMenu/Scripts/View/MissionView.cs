using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionView : View
{
	public RawImage missionImage;
	public Text missionName;
	public Text enemyCountLabel;
	public Text goldRewardLabel;
	public Text expRewardLabel;

	public void OnClickMissionPoint(string stageID) {
		((MissionController)controller).GoToMission(((MissionModel)model).missionDictionary[stageID]);
	}

	public void OnClickCloseButton() 
	{
		((MissionController)controller).TransitionOut();
	}

	public void OnClickFightButton() 
	{
		((MissionController)controller).StartMissionMatch();
	}
}
