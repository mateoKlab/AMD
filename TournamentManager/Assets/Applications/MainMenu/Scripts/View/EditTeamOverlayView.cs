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

		editTeamController = transform.parent.GetComponent<EditTeamController>();
	}

	#region IDropHandler implementation
	
	public void OnDrop (PointerEventData eventData)
	{
		//Debug.LogError("EditTeamView OnDrop");
		editTeamController.ReturnTroopFromSlotToTeamPanel(eventData.selectedObject);
	}
	
	#endregion
}
