using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionPointView : View
{
	public void OnClickMissionPoint() 
	{
		((MissionPointController)controller).ShowMissionPopUp();
	}
}
