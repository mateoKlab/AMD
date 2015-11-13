using UnityEngine;
using System.Collections;
using Bingo;

public class MissionPointModel : Model
{
	public string imagePath = "Sprites/StagePreviewSprites/";
	public StageData missionPointData;


	public void SetStageData(StageData sData) {
		missionPointData = sData;
	}
}
