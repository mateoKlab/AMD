using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class ActiveTeamSlotView : View, IDropHandler
{
	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		((ActiveTeamSlotController)controller).SetTroopOnSlot(eventData.selectedObject);
	}

	#endregion

}
