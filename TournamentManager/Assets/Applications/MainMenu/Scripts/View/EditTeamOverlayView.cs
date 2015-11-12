using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class EditTeamOverlayView : View<MainMenu>, IDropHandler
{
	private EditTeamController editTeamController;

	public override void Awake()
	{
		base.Awake();

		editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();
	}

	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		//Debug.LogError("EditTeamView OnDrop");
		if(eventData.selectedObject != null)
			editTeamController.ReturnTroopFromSlotToTeamPanel(eventData.selectedObject);
	}
	
	#endregion
}
