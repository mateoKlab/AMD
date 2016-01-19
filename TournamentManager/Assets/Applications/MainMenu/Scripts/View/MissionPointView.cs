using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class MissionPointView : View
{
	public void OnClickMissionPoint() 
	{
		SoundManager.instance.PlayUISFX("Audio/SFX/Button2");
		((MissionPointController)controller).ShowMissionPopUp();
	}
}
