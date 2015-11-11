using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class HeaderView : View
{
	private Text goldLabel;

	public override void Awake() {
		base.Awake();
		InitializeHeader();
	}

	private void InitializeHeader() 
	{
		goldLabel = transform.FindChild("GoldLabel").GetComponent<Text>();
		UpdateGoldValue();
	}

	public void UpdateGoldValue() {
		goldLabel.text = "GOLD: " + GameData.instance.playerData.gold;
	}
}
