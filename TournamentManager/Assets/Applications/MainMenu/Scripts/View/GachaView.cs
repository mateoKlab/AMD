using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class GachaView : View
{
	private Text hpLabel;
	private Text atkLabel;
	private Button rollButton;
	private RawImage characterSprite;

	public override void Awake() {
		base.Awake();
		InitializeGachaInterface();
	}

	private void InitializeGachaInterface() {
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

	public void DisplayGachaCharacter(int hp, int atk) {
		hpLabel.text = "HP: " + hp;
		atkLabel.text = "ATK: " + atk;
		characterSprite.gameObject.SetActive(true);
	}
}
