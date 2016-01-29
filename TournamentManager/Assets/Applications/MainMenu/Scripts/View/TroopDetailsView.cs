using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopDetailsView : View
{
	public Text nameText;
	public Text lvlText;
	public Text classText;
	public Text atkText;
	public Text hpText;
	public Text defText;
	public Text costText;
	public RawImage classIcon;
	private Texture warriorIconTexture;
	private Texture mageIconTexture;

	// TODO: Load appropriate prefab from resources.
    public FighterSpriteController warriorSprite;
	public FighterSpriteController mageSprite;

	public void Awake() {
		base.Awake();
		warriorIconTexture = Resources.Load("Sprites/ClassIcons/Warrior") as Texture;
		mageIconTexture = Resources.Load("Sprites/ClassIcons/Mage") as Texture;
	}

    public void SetName(string name)
    {
       	if (nameText != null)
		{
			nameText.text = name;
		}
    }

	public void SetLevel(int lvl)
	{
		if (lvlText != null)
		{
			lvlText.text = "Lvl " + lvl.ToString();
		}
	}

	public void SetIcon(string fighterClass)
	{
		if (classIcon != null)
		{
			if (fighterClass == "Warrior") 
			{
				classIcon.texture = warriorIconTexture;
			}
			else if (fighterClass == "Mage") {
				classIcon.texture = mageIconTexture;
			}

		}
	}

	public void SetClass(string className)
	{
		if (classText != null)
		{
			classText.text = className;
		}
	}

    public void SetAtk(float atk)
    {
		if (atkText != null) 
		{
			atkText.text = Mathf.RoundToInt(atk).ToString("n0");
		}

    }

	public void SetDef(float atk)
	{
		if (defText != null) 
		{
			defText.text = Mathf.RoundToInt(atk).ToString("n0");
		}
		
	}
	
	public void SetHP(float hp)
    {
		if (hpText != null) {
			hpText.text = Mathf.RoundToInt(hp).ToString("n0");
		}       
    }

    public void SetCost(int cost)
    {
		if (costText != null) 
		{
			costText.text = cost.ToString("n0");
		}
    }

    public void SetSprite(Class fighterClass, FighterSkinData skinData)
	{
		mageSprite.gameObject.SetActive (false);
		warriorSprite.gameObject.SetActive (false);

		switch (fighterClass) {
		case Class.Warrior:
		{
			warriorSprite.SetFighterSkin (skinData);
			warriorSprite.gameObject.SetActive (true);
			break;
		}

		case Class.Mage:
		{
			mageSprite.SetFighterSkin (skinData);
			mageSprite.gameObject.SetActive (true);
			break;
		}

		default:
		{
			warriorSprite.SetFighterSkin (skinData);
			warriorSprite.gameObject.SetActive (true);
			break;
		}

		}

//		if (troopSprite != null) {
//
//			// TODO building sprite everytime feels really slow, refactor later
//			troopSprite.SetFighterSkin(skinData);
//			// 9162166868
//		}
    }

	public void HideSprites() {
		mageSprite.gameObject.SetActive (false);
		warriorSprite.gameObject.SetActive (false);
	}
	
}
