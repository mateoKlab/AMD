using UnityEngine;
using System.Collections;
using Bingo;

public class ArmoryItemController : Controller <MainMenu, ArmoryItemModel, ArmoryItemView>
{
	public void DisplayArmoryItem(Equipment item, string type) {

		model.currentEquipment = item;

		view.itemSprite.texture = Resources.Load("Sprites/UnitSprites/" + type + "/" + item.spriteName) as Texture;

		if (app.model.armoryModel.unlockedItems.Contains(item)) // Do if item is unlocked
		{
			view.itemSprite.color = Color.white;
			view.itemLabel.text = item.name;
			view.diamondsIcon.SetActive(false);
		} 
		else 
		{
			view.itemSprite.color = Color.black;
			view.itemLabel.text = "100";
			view.diamondsIcon.SetActive(true);
		}
	}

	public void UnlockItem() {
		int cost = 100;
		if (GameData.instance.playerData.diamonds < cost)
			return;

		if (!app.model.armoryModel.unlockedItems.Contains(model.currentEquipment))
		{
			app.model.armoryModel.unlockedItems.Add(model.currentEquipment);
			GameData.instance.playerData.diamonds -= cost;
			app.view.headerView.UpdateDiamondsValue();
			
			view.itemSprite.color = Color.white;
			view.itemLabel.text = model.currentEquipment.name;
			view.diamondsIcon.SetActive(false);
		}

	}
}
