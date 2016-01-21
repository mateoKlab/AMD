using UnityEngine;
using System.Collections;
using Bingo;

public class EquipmentTypeCellController : EquipmentCellController
{
	public string typeString;

	public void OnClickTypeCell() {
		app.controller.editEquipmentController.LoadEquipment(typeString);
	}
}
