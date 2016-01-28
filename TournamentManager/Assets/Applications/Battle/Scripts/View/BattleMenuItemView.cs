using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemView : View <Battle, BattleMenuItemModel, BattleMenuItemController>
{
	public Text fighterName;
	//public Text hpLabel;
	public RawImage classIcon;
	public Image hpSlider;
	public Image deathIcon;
	public FighterSpriteController warriorPortrait;
	public FighterSpriteController magePortrait;

	void Start ()
	{

	}

	public void InitializeValues ()
	{
		model.fData = model.fighter.GetComponent<FighterModel>().fighterData;
		fighterName.text = model.fData.name;
		classIcon.texture = classIcon.texture = Resources.Load("Sprites/ClassIcons/" + model.fData.fighterClass) as Texture;

		UpdateHP();
		SetPortrait(model.fData.fighterClass, model.fData.skinData);
	}

	public void UpdateHP() {
		float hpFill = (float)model.fData.HP / (float)model.fData.maxHP;
		
		// TODO: Animate.. Gradually decrease fill amount.
		hpSlider.fillAmount = hpFill;
	}

	public void ShowDeathIcon (bool enabled)
	{
		deathIcon.gameObject.SetActive (enabled);
	}

	private void SetPortrait (Class fighterClass, FighterSkinData skinData)
	{
		warriorPortrait.gameObject.SetActive (false);
		magePortrait.gameObject.SetActive (false);
		
		switch (fighterClass) {
		case Class.Warrior:
		{
			warriorPortrait.SetFighterSkin (skinData);
			warriorPortrait.gameObject.SetActive (true);
			break;
		}
			
		case Class.Mage:
		{
			
			magePortrait.SetFighterSkin (skinData);
			magePortrait.gameObject.SetActive (true);
			break;
		}
			
		}
	}
}
