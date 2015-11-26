using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class TroopView : View<MainMenu>, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    private FighterSpriteController sprite;
    private Canvas myCanvas;

	public override void Awake ()
	{
		base.Awake ();

        sprite = transform.GetChild(0).GetComponent<FighterSpriteController>();
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

    public void SetIcon()
    {
        SpriteBuilder.instance.BuildSprite(sprite);
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
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        transform.position = myCanvas.transform.TransformPoint(pos);
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
