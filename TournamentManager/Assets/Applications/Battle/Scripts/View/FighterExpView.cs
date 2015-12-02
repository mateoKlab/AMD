using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class FighterExpView : View<MainMenu, FighterModel, FighterExpController>
{
    private RawImage levelupBanner;
    private FighterSpriteController troopSprite;
    private Text nameText;
    private Text levelText;
    private Text expGainedText;
    private Slider expSlider;
    
    public override void Awake ()
    {
        base.Awake ();

        levelupBanner = transform.Find("Banner").GetComponent<RawImage>();
        troopSprite = transform.Find("TroopSprite").GetComponent<FighterSpriteController>();
        nameText = transform.Find("Name").GetComponent<Text>();
        levelText = transform.Find("Level").GetComponent<Text>();
        expGainedText = transform.Find("ExpGained").GetComponent<Text>();
        expSlider = transform.Find("ExpSlider").GetComponent<Slider>();
    }

    public void EnableLevelupBanner(bool enabled)
    {
        levelupBanner.gameObject.SetActive(enabled);
    }

    public void SetSprite(FighterSkinData skinData)
    {
        troopSprite.SetFighterSkin(skinData);
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetLevel(int level)
    {
        levelText.text = "LEVEL " + level;
    }

    public void SetExpGained(float expGained)
    {
        expGainedText.text = "EXP GAINED " + Mathf.RoundToInt(expGained);
    }

    public void SetCurrentExpOnSlider(float expRatio)
    {
        expSlider.value = expRatio;
    }
}
