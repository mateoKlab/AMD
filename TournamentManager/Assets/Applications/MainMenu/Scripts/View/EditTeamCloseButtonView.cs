using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class EditTeamCloseButtonView : View<MainMenu>, IPointerClickHandler
{
	#region IPointerClickHandler implementation

	public void OnPointerClick (PointerEventData eventData)
	{
		app.controller.editTeamController.HideEditTeam();
	}

	#endregion


}
