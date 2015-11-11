using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopDetailsView : View
{
	private Text nameText;
	private Text atkText;
	private Text hpText;

	public override void Awake()
	{
		base.Awake();
		nameText = transform.FindChild("Name").GetComponent<Text>();
		atkText = transform.FindChild("HP").GetComponent<Text>();
		hpText = transform.FindChild("Attack").GetComponent<Text>();
	}

	public void SetName(string name) 
	{
		nameText.text = name;
	}

	public void SetAtk(float atk)
	{
		atkText.text = Mathf.RoundToInt(atk).ToString("n0");
	}

	public void SetHP(float hp)
	{
		hpText.text = Mathf.RoundToInt(hp).ToString("n0");
	}

}
