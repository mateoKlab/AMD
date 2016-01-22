using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class EquipmentItemCellController : EquipmentCellController
{

	public void SetItemDetails(Equipment equipment) {
		model.equipment = equipment;
		if (app.model.editEquipmentModel.fighterToEdit == null) 
			Debug.LogError("NO");
		else
			Debug.LogError(app.model.editEquipmentModel.fighterToEdit.fighterClass.ToString());
		Texture spriteTexture = Resources.Load("Sprites/UnitSprites/" + app.model.editEquipmentModel.fighterToEdit.fighterClass.ToString() + "/" + equipment.sprites[0].attachmentType + "/" + equipment.sprites[0].spriteName) as Texture;
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
