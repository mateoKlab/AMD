using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class GachaView : View
{
	private Text nameLabel;
	private Text hpLabel;
	private Text atkLabel;
	private Button rollButton;
	private RawImage characterSprite;

	public override void Awake() {
		base.Awake();
		InitializeGachaInterface();
	}

	private void InitializeGachaInterface() 
	{
		nameLabel = transform.FindChild("Name Label").GetComponent<Text>();
		atkLabel = transform.FindChild("ATK Label").GetComponent<Text>();
		hpLabel = transform.FindChild("HP Label").GetComponent<Text>();
		rollButton = transform.FindChild("RollButton").GetComponent<Button>();
		characterSprite = transform.FindChild("GachaCharacterSprite").GetComponent<RawImage>();
	}

	public void OnClickCloseButton() {
		((GachaController)controller).CloseGachaPopUp();
	}

	public void OnClickRollButton() {
		((GachaController)controller).GenerateRandomCharacter();
		//rollButton.interactable = false;
	}

	public void DisplayGachaCharacter(string name, int hp, int atk) {
		nameLabel.text = "Name: " + name;
		hpLabel.text = "HP: " + hp;
		atkLabel.text = "ATK: " + atk;
		characterSprite.gameObject.SetActive(true);
	}
}
