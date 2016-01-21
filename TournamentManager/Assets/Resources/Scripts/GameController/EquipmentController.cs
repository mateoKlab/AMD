using UnityEngine;
using System.Collections;

public class EquipmentController {

	private EquipmentDatabase equipmentDatabase;
	private PlayerData playerData;

	public EquipmentController (EquipmentDatabase equipmentDatabase, PlayerData playerData)
	{
		this.equipmentDatabase = equipmentDatabase;
		this.playerData = playerData;
	}

	public void UnlockEquipment (string id)
	{
		if (!playerData.unlockedEquipment.Contains (id)) {
			playerData.unlockedEquipment.Add (id);
		}
	}


}
