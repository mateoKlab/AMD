using UnityEngine;
using System.Collections;
using Bingo;

public class BarracksController : Controller<MainMenu, BarracksModel, BarracksView>
{
    private GameData gameData;
    private PlayerData playerData;
    private TownData town;

    public override void Awake()
    {
        base.Awake();

        gameData = GameData.instance;
        playerData = gameData.playerData;
        town = gameData.town;
    }

    void OnEnable()
    {
        InitFacility(1);
    }

    public void InitFacility(int level)
    {
        model.level = town.barracksLevel;
        model.cost = model.level * 100;
        model.cooldown = 2f;
        view.buildButton.gameObject.SetActive(CheckIfUpgradable());
        UpdateFacilitySprite(model.level);

        if(model.level > 0)
        {
            view.facilitySprite.gameObject.SetActive(true);
        }
    }
    
    public bool CheckIfUpgradable()
    {
        if (model.cost <= playerData.gold && model.level < TownData.MAX_BARRACKS_LEVEL)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void StartBuildingTownFacility() 
    {
        if (CheckIfUpgradable() == false)
        {
            return;
        }

        app.view.headerView.UpdateGoldValue();
        model.timeClicked = System.DateTime.Now;
        view.hammer.gameObject.SetActive(true);
        view.buildButton.gameObject.SetActive(false);
        view.progressSlider.gameObject.SetActive(true);
        app.controller.townController.CheckFacilityButtons();

        StopCoroutine("AnimateBuilding");
        StartCoroutine("AnimateBuilding");
    }

    private IEnumerator AnimateBuilding()
    {
        view.progressSlider.value = 0;
        while(view.progressSlider.value < 1)
        {
            view.progressSlider.value = (float)(System.DateTime.Now - model.timeClicked).TotalSeconds / model.cooldown;
            yield return new WaitForEndOfFrame();
        }
        
        OnFinishBuilding();
    }

    public void OnFinishBuilding()
    {
        ApplyUpgrade();

        view.progressSlider.gameObject.SetActive(false);
        view.hammer.gameObject.SetActive(false);
        view.facilitySprite.gameObject.SetActive(true);
        view.buildButton.gameObject.SetActive(CheckIfUpgradable());
        view.UpdateCost();
        UpdateFacilitySprite(model.level);
    }

    public void ApplyUpgrade()
    {
        playerData.gold -= model.cost;
        model.level++;
        town.barracksLevel = model.level;
        model.cost += 100;
        GameData.instance.Save();
    }

    public void UpdateFacilitySprite(int upgradeLevel)
    {
        if(upgradeLevel == 0)
        {
            return;
        }

        Sprite sprite = Resources.Load<Sprite>("Sprites/Town/barracks_" + upgradeLevel);
        view.UpdateFacilitySprite(sprite);
    }
}
