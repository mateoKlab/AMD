using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class EquipmentCellView : View <MainMenu, EquipmentCellModel, EquipmentCellController>
{
	public RawImage itemSprite;

	public void OnClickEquipmentItemButton() {
		((EquipmentItemCellController)controller).EquipItem();
	}
}
