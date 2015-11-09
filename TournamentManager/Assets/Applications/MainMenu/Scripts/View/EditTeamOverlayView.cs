using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class EditTeamOverlayView : View<MainMenu>, IDropHandler
{
	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		Debug.LogError("EditTeamView OnDrop");
		app.controller.editTeamController.ReturnTroopFromSlotToTeamPanel(eventData.selectedObject);
	}
	
	#endregion
}
