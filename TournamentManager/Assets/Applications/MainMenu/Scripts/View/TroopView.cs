using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class TroopView : View, IBeginDragHandler, IDragHandler, IEndDragHandler
{

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
}
