using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class FacilityController : Controller <MainMenu, FacilityModel, FacilityView>
{
	private bool isBuilding;

	public void Update() {
		if (isBuilding)
		{
			view.progressSlider.value = (float)(System.DateTime.Now - model.timeClicked).TotalSeconds / model.cooldown;//+= Time.deltaTime/((FacilityModel)model).cooldown;
	
		}

		if (view.progressSlider.value >= 1) {
			view.progressSlider.gameObject.SetActive(false);
			view.hammer.gameObject.SetActive(false);
			view.facility.SetActive(true);
		}
	}

	public void StartBuildingTownFacility() 
	{
		if (GameData.instance.playerData.gold < model.cost)
		{
			return;
		}

		GameData.instance.playerData.gold -= model.cost;
		app.view.headerView.UpdateGoldValue();
		model.timeClicked = System.DateTime.Now;
		view.hammer.gameObject.SetActive(true);
		view.buildButton.gameObject.SetActive(false);
		view.progressSlider.gameObject.SetActive(true);
		app.controller.townController.CheckFacilityButtons();
		isBuilding = true;
	}

	public void CheckGold()
	{
		if (GameData.instance.playerData.gold < model.cost)
		{
			view.buildButton.interactable = false;
		}
	}
}
