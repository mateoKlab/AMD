using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class FighterExpView : View<MainMenu, FighterModel, FighterExpController>
{
    //private RawImage levelupBanner;
    //private FighterSpriteController troopSprite;
    public Text nameLabel;
	public Text levelText;
	//public Text expGainedText;
	public Image expMeter;

    public void EnableLevelupBanner(bool enabled)
    {
        //levelupBanner.gameObject.SetActive(enabled);
    }

    public void SetSprite(FighterSkinData skinData)
    {
        //troopSprite.SetFighterSkin(skinData);
    }

    public void SetName(string name)
    {
        nameLabel.text = name;
    }

    public void SetLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void SetExpGained(float expGained)
    {
        //expGainedText.text = "EXP Gained: " + Mathf.RoundToInt(expGained);
		//Debug.LogError ("ExP: " +  expGained); 
    }

    public void SetSliderValue(float expPercentage)
    {
		expMeter.fillAmount = expPercentage;
    }

}
