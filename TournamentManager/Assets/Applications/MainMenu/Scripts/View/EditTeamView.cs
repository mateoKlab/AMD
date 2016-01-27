using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class EditTeamView : View <MainMenu, EditTeamModel, EditTeamController>
{
	public GameObject partyCostBar;
	public List<RawImage> slots;
	public Text teamCostLabel;
	public Text teamSlotsLabel;

	public void SetCost(int cost, int capacity)
	{
		teamCostLabel.text = cost + "/" + capacity;
		teamSlotsLabel.text = (GameData.instance.playerData.currentParty.fighters.Count + "/" + GameData.MAX_ACTIVE_FIGHTERS);

		for (int  i = 0; i < capacity; i++) {
			if (i < cost)
			{
				slots[i].color = new Color32(255, 195, 0, 255);
			}
			else
			{
				slots[i].color = new Color32(215, 85, 45, 255);
			}
		}
	}

	public void OnClickEditEquimentButton() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		controller.ShowEditEquipmentScreen();
	}
}
