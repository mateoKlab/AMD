using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class TroopView : View<MainMenu>, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	private Image icon;

	public override void Awake ()
	{
		base.Awake ();

		icon = gameObject.GetComponent<Image>();
	}

	public void SetIcon(Sprite icon)
	{
		this.icon.sprite = icon;
	}

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
		if(eventData.selectedObject != null)
			((TroopController)controller).OnDrop(eventData.selectedObject);
	}
}
