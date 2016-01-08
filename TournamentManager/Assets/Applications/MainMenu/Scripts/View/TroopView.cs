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

    void Start()
    {
//        SetSprite(((FighterModel)model).skindData);
    }

    public void SetSprite(FighterSkinData skinData)
    {
        //troopSprite.SetFighterSkin(skinData);
    }

	public void OnClickStateButton() {
		((TroopController)controller).ToggleState();
	}

	public void OnClickTroop() {
		((TroopController)controller).DisplayTroopDetails();
	}
}
