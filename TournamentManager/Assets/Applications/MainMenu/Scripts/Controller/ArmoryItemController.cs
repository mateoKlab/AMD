using UnityEngine;
using System.Collections;
using Bingo;

public class ArmoryItemController : Controller <MainMenu, ArmoryItemModel, ArmoryItemView>
{
	public void UpdateArmoryItem(string spriteName, string itemName, string type) {
		view.itemLabel.text = spriteName;
		view.itemSprite.texture = Resources.Load("Sprites/UnitSprites/" + type + "/" + spriteName) as Texture;
	}
}
