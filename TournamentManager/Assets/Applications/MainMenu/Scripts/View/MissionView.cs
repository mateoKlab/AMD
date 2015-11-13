using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionView : View
{
	public RawImage missionImage;
	public Text missionName;

	public void OnClickMissionPoint(string stageID) {
		((MissionController)controller).GoToMission(((MissionModel)model).missionDictionary[stageID]);
	}
	
	public override void Awake ()
	{
		base.Awake();
		missionImage = transform.FindChild("MissionImage").GetComponent<RawImage>();
		missionName = transform.FindChild("MissionName").GetComponent<Text>();
	}

	public void OnClickCloseButton() 
	{
		((MissionController)controller).CloseMissionPopUp();
	}

	public void OnClickFightButton() 
	{
		((MissionController)controller).StartMissionMatch();
	}
}
