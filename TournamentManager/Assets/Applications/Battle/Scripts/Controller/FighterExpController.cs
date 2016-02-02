using UnityEngine;
using System.Collections;
using Bingo;

public class FighterExpController : Controller
{
    private bool isActive;
	public FighterSpriteController warriorFSController;
	public FighterSpriteController mageFSController;
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

		((FighterExpView)view).SetClassIcon(fData);
		((FighterExpView)view).SetName(fData.name);
		((FighterExpView)view).SetLevel(fData.level);
        ((FighterExpView)view).SetExpGained(GameData.instance.currentStage.xpReward);

		if (fData.fighterClass == Class.Warrior) 
		{
			mageFSController.gameObject.SetActive(false);
		
			warriorFSController.gameObject.SetActive(true);
			warriorFSController.SetFighterSkin(fData.skinData);
		}
		else
		{
			warriorFSController.gameObject.SetActive(false);

			mageFSController.gameObject.SetActive(true);
			mageFSController.SetFighterSkin(fData.skinData);
		}


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
