using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class EditTeamView : View
{
	public GameObject partyCostBar;
	public List<RawImage> slots;
	private Text teamCostLabel;
	private Text teamSlotsLabel;

	public override void Awake ()
	{
		base.Awake ();

		teamCostLabel = transform.Find("PartyCost").GetComponent<Text>();
		teamSlotsLabel = transform.Find("PartySlots").GetComponent<Text>();
	}

	public void SetCost(int cost, int capacity)
	{
		teamCostLabel.text = cost + " / " + capacity;
		teamSlotsLabel.text = (GameData.instance.playerData.currentParty.fighters.Count + " / " + GameData.MAX_ACTIVE_FIGHTERS);

		for (int  i = 0; i < capacity; i++) {
			if (i < cost)
			{
				slots[i].color = new Color32(215, 85, 45, 255);
			}
			else
			{
				slots[i].color = new Color32(255, 195, 0, 255);
			}
		}
	}
}
