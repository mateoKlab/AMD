using UnityEngine;
using System.Collections;
using Bingo;

public class ArmoryItemController : Controller <MainMenu, ArmoryItemModel, ArmoryItemView>
{
	public void DisplayArmoryItem(Equipment item, string type) {

		model.currentEquipment = item;

		view.itemSprite.texture = Resources.Load("Sprites/UnitSprites/" + type + "/" + item.spriteName) as Texture;

		if (GameData.instance.playerData.unlockedEquipment.Contains (item.id)) 
		{
			view.itemSprite.color = Color.white;
			view.itemLabel.text = item.name;
			view.diamondsIcon.SetActive(false);
		}
	}

	public void UnlockItem() {
		int cost = 100;
		if (GameData.instance.playerData.diamonds < cost) {
			return;
		}

		GameData.instance.playerData.UnlockEquipment (model.currentEquipment.id);

		GameData.instance.playerData.diamonds -= cost;
		app.view.headerView.UpdateDiamondsValue();
		
		view.itemSprite.color = Color.white;
		view.itemLabel.text = model.currentEquipment.name;
		view.diamondsIcon.SetActive(false);
	}
}
