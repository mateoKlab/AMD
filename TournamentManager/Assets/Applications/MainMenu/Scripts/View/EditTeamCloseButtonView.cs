using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class EditTeamCloseButtonView : View<MainMenu>, IPointerClickHandler
{
	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		app.controller.editTeamController.HideEditTeam();
	}

	#endregion


}
