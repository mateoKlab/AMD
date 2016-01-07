using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using Bingo;

public class TroopView : View<MainMenu> //, IPointerEnterHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
	public Text nameLabel;
	public RawImage classIcon;
	public Text stateLabel;
    private FighterSpriteController troopSprite;
    private Canvas myCanvas;

	public override void Awake ()
	{
		base.Awake ();

        //troopSprite = transform.GetChild(0).GetComponent<FighterSpriteController>();
        myCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
	}

    void Start()
    {
        SetSprite(((FighterModel)model).skindData);
    }

    public void SetSprite(FighterSkinData skinData)
    {
        //troopSprite.SetFighterSkin(skinData);
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
		return;
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

	public void OnClickStateButton() {
		((TroopController)controller).ToggleState();
	}

	public void OnClickTroop() {
		((TroopController)controller).DisplayTroopDetails();
	}
}
