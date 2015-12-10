using UnityEngine;
using System.Collections.Generic;
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
	public List <GachaSpriteMap> gachaSprites =  new List<GachaSpriteMap>();

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
	}

	public void DisplayGachaCharacter(FighterData fData) {
		nameLabel.text = "Name: " + fData.name;
		hpLabel.text = "HP: " + fData.HP;
		atkLabel.text = "ATK: " + fData.ATK;
		classLabel.text = "Class: " + fData.fighterClass;
		elementLabel.text = "Element: " + fData.fighterElement;

		classIcon.texture = Resources.Load("Sprites/ClassIcons/" + fData.fighterClass) as Texture;
		classIcon.gameObject.SetActive(true);
		for (int i = 0; i < gachaSprites.Count; i++)
		{
			if(gachaSprites[i].characterClass == fData.fighterClass)
			{
				gachaSprites[i].characterSprite.SetActive(true);
				gachaSprites[i].characterSprite.GetComponent<FighterSpriteController>().SetFighterSkin (fData.skinData);
			}
			else
			{
				gachaSprites[i].characterSprite.SetActive(false);
			}
		}
	}

	public void ResetDisplayValues() {
		nameLabel.text = "";
		hpLabel.text = "";
		atkLabel.text = "";
		classLabel.text = "";
		elementLabel.text = "";
		classIcon.gameObject.SetActive(false);
	
		for (int i = 0; i < gachaSprites.Count; i++)
		{
			gachaSprites[i].characterSprite.SetActive(false);
		}
	}

	public void OnEnable() 
	{
		((GachaController)controller).CheckGold();
	}

}
