using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;
using Spine;

public class GachaView : View
{
	private Text nameLabel;
	private Text classLabel;
	private Text hpLabel;
	private Text atkLabel;
	private Text elementLabel;
	private RawImage classIcon;
	public Button rollButton;
	public GameObject characterSprite;

	public override void Awake() {
		base.Awake();
		InitializeGachaInterface();
	}

	private void InitializeGachaInterface() 
	{
		nameLabel = transform.FindChild("NameLabel").GetComponent<Text>();
		classLabel = transform.FindChild("ClassLabel").GetComponent<Text>();
		hpLabel = transform.FindChild("HPLabel").GetComponent<Text>();
		atkLabel = transform.FindChild("ATKLabel").GetComponent<Text>();
		elementLabel = transform.FindChild("ElementLabel").GetComponent<Text>();
		rollButton = transform.FindChild("RollButton").GetComponent<Button>();
		classIcon = transform.FindChild("ClassIcon").GetComponent<RawImage>();
	}

	public void OnClickCloseButton() {
		ResetDisplayValues();
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
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

		classIcon.texture = Resources.Load("Sprites/ClassIcons/" + fData.fighterClass) as Texture;
		classIcon.gameObject.SetActive(true);
		characterSprite.SetActive (true);
		characterSprite.GetComponent<FighterSpriteController> ().SetFighterSkin (fData.skinData);
//		SpriteBuilder.instance.BuildSprite (characterSprite.GetComponent <FighterSpriteController> ());
	}

	public void ResetDisplayValues() {
		nameLabel.text = "";
		hpLabel.text = "";
		atkLabel.text = "";
		classLabel.text = "";
		elementLabel.text = "";
		classIcon.gameObject.SetActive(false);
		characterSprite.SetActive (false);
	}

	public void OnEnable() 
	{
		((GachaController)controller).CheckGold();
	}
}
