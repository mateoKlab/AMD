using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class GachaView : View
{
	public Text nameLabel;
	public Text classLabel;
	public Text hpLabel;
	public Text atkLabel;
	public Text elementLabel;
	public Button rollButton;
	public RawImage characterSprite;
	private string spritePath = "Sprites/GachaSprites/";

	public override void Awake() {
		base.Awake();
		InitializeGachaInterface();
	}

	private void InitializeGachaInterface() 
	{
		characterSprite = transform.FindChild("CharacterSprite").GetComponent<RawImage>();
		nameLabel = transform.FindChild("NameLabel").GetComponent<Text>();
		classLabel = transform.FindChild("ClassLabel").GetComponent<Text>();
		hpLabel = transform.FindChild("HPLabel").GetComponent<Text>();
		atkLabel = transform.FindChild("ATKLabel").GetComponent<Text>();
		elementLabel = transform.FindChild("ElementLabel").GetComponent<Text>();
		rollButton = transform.FindChild("RollButton").GetComponent<Button>();
		characterSprite.texture = Resources.Load(spritePath + "Default") as Texture;
	}

	public void OnClickCloseButton() {
		ResetDisplayValues();
		((GachaController)controller).CloseGachaPopUp();
	}

	public void OnClickRollButton() {
		((GachaController)controller).GenerateRandomCharacter();
		//rollButton.interactable = false;
	}

	public void DisplayGachaCharacter(FighterData fData) {
		nameLabel.text = "Name: " + fData.name;
		hpLabel.text = "HP: " + fData.HP;
		atkLabel.text = "ATK: " + fData.ATK;
		classLabel.text = "Class: " + fData.fighterClass;
		elementLabel.text = "Element: " + fData.fighterElement;
		characterSprite.texture = Resources.Load(spritePath + fData.fighterElement) as Texture;
	}

	public void ResetDisplayValues() {
		nameLabel.text = "";
		hpLabel.text = "";
		atkLabel.text = "";
		classLabel.text = "";
		elementLabel.text = "";
		characterSprite.texture = Resources.Load(spritePath + "Default") as Texture;
	}
}
