using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class TroopView : View<MainMenu>, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public void OnPointerEnter (PointerEventData eventData)
	{
		((TroopController)controller).OnPointerEnter();
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		((TroopController)controller).OnBeginDrag();
	}

	public void OnDrag(PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}

	public void OnEndDrag(PointerEventData eventData) 
	{
		((TroopController)controller).OnEndDrag();
	}

	// This event is called when a troop is dropped over another troop.
	public void OnDrop (PointerEventData eventData)
	{
		((TroopController)controller).OnDrop(eventData.selectedObject);
	}
}
