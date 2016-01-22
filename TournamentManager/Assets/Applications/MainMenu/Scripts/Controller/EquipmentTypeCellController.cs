using UnityEngine;
using System.Collections;
using Bingo;

public class EquipmentTypeCellController : EquipmentCellController
{
	public string typeString;

	public void OnClickTypeCell() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		app.controller.editEquipmentController.LoadEquipment(typeString);
	}
}
