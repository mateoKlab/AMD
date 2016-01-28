using UnityEngine;
using System.Collections.Generic;
using Bingo;
using UnityEngine.UI;
using Spine;

public class GachaView : View <MainMenu, GachaModel, GachaController>
{
	public Text nameLabel;
	public Text classLabel;
	public Text hpLabel;
	public Text atkLabel;
	public Text defLabel;
	public Text costLabel;
	public Text elementLabel;
	public RawImage classIcon;
	public Button rollButton;
	public List <GachaSpriteMap> gachaSprites =  new List<GachaSpriteMap>();

	public void OnClickCloseButton() {
		ResetDisplayValues();
		controller.CloseGachaScreen();

	}

	public void OnClickRollButton() {
		((GachaController)controller).GenerateRandomCharacter();
	}

	public void DisplayGachaCharacter(FighterData fData) {
		foreach (GachaSpriteMap sprite in gachaSprites) {
			sprite.characterSprite.SetActive (false);
		}

		nameLabel.text = "Name: " + fData.name;
		hpLabel.text = "HP: " + fData.HP;
		atkLabel.text = "ATK: " + fData.ATK;
		defLabel.text = "DEF: " + fData.DEF;
		costLabel.text = "Cost: " + fData.cost;
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

//				gachaSprites[i].characterSprite.SetActive(false);
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
