using UnityEngine;
using System.Collections;
using Bingo;

public class FighterExpController : Controller
{
    private bool isActive;
	public FighterSpriteController fsController;
	public FighterData fData;

    void Start()
    {
        ((FighterExpView)view).EnableLevelupBanner(false);
        gameObject.SetActive(isActive);
    }

    public void EnableFighter(bool enabled)
    {
        isActive = enabled;
        gameObject.SetActive(isActive);
    }

    public void SetFighterDetails(FighterData fd)
    {
        ((FighterExpView)view).SetSprite(fd.skinData);
        ((FighterExpView)view).SetName(fd.name);
        ((FighterExpView)view).SetLevel(fd.level);
        ((FighterExpView)view).SetExpGained(GameData.instance.currentStage.xpReward);
		fsController.SetFighterSkin(fd.skinData);
		fData = fd;
        //((FighterExpView)view).SetSliderValue(Random.value);
    }
	

	public void AddExp(int amount) {
		fData.xp += amount;

		((FighterExpView)view).SetSliderValue((float)fData.xp/3000);

	}

	public void LevelUp() {

	}
}
