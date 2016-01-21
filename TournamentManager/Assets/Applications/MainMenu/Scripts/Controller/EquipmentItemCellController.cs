using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class EquipmentItemCellController : EquipmentCellController 
{
	public void SetItemDetails(Equipment equipment) {
		Debug.LogError (equipment.sprites[0].attachmentType + "   " + equipment.sprites[0].spriteName);
		Texture spriteTexture = Resources.Load("Sprites/UnitSpritesUpdated/" + equipment.sprites[0].attachmentType + "/" + equipment.sprites[0].spriteName) as Texture;
		view.itemSprite.texture = spriteTexture;
		view.itemSprite.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(spriteTexture.width * .3f,spriteTexture.height * .3f);
	}
}
