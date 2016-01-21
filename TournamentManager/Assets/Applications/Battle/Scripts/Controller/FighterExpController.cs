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
		fData = fd;

		((FighterExpView)view).SetSprite(fData.skinData);
		((FighterExpView)view).SetName(fData.name);
		((FighterExpView)view).SetLevel(fData.level);
        ((FighterExpView)view).SetExpGained(GameData.instance.currentStage.xpReward);
		fsController.SetFighterSkin(fData.skinData);

        //((FighterExpView)view).SetSliderValue(Random.value);
    }
	

	public void AddExp(int amount) {
		fData.exp += amount;

		if (GameController.levelUpController.CheckLevelUp (fData)) {
			SoundManager.instance.PlayUISFX("Audio/SFX/LevelUp");
			GetComponent<Animator>().SetTrigger("LevelUp");

			GameController.levelUpController.LevelUp (fData);
			((FighterExpView)view).SetLevel(fData.level);
		}

		((FighterExpView)view).SetSliderValue (GameController.levelUpController.GetNextLevelProgress (fData));

	}

	public void LevelUp() {

	}
}
