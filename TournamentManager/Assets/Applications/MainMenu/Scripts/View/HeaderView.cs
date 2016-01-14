using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class HeaderView : View
{
	private Text goldLabel;
	private Text diamondsLabel;

	public override void Awake() {
		base.Awake();
		InitializeHeader();
	}

	private void InitializeHeader() 
	{
		goldLabel = transform.FindChild("GoldLabel").GetComponent<Text>();
		diamondsLabel = transform.FindChild("DiamondsLabel").GetComponent<Text>();
		UpdateGoldValue();
		UpdateDiamondsValue();
	}

	public void UpdateGoldValue() 
	{
		goldLabel.text = "Gold: " + GameData.instance.playerData.gold;
	}

	public void UpdateDiamondsValue() 
	{
		diamondsLabel.text = "Diamonds: " + GameData.instance.playerData.diamonds;
	}
}
