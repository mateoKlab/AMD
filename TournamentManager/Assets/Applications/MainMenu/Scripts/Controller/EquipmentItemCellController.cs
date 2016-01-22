using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class EquipmentItemCellController : EquipmentCellController
{

	public void SetItemDetails(Equipment equipment) {
		model.equipment = equipment;
		Texture spriteTexture = Resources.Load("Sprites/UnitSpritesUpdated/" + equipment.sprites[0].attachmentType + "/" + equipment.sprites[0].spriteName) as Texture;
		view.itemSprite.texture = spriteTexture;
		view.itemSprite.GetComponent<RawImage>().rectTransform.sizeDelta = new Vector2(spriteTexture.width * .3f,spriteTexture.height * .3f);
	}

	public void EquipItem() {
		foreach (EquipmentSprite eSprite in model.equipment.sprites) {
			app.model.editEquipmentModel.fighterToEdit.skinData.ReplaceSkin(eSprite.attachmentType, eSprite.spriteName);
		}

		app.controller.editEquipmentController.fSpriteController.SetFighterSkin(app.model.editEquipmentModel.fighterToEdit.skinData);
	}
}
