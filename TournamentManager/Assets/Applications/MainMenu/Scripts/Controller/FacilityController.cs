using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class FacilityController : Controller
{
	private bool isBuilding;

	public void Update() {
		if (isBuilding)
		{
			((FacilityView)view).progressSlider.value = (float)(System.DateTime.Now - ((FacilityModel)model).timeClicked).TotalSeconds / ((FacilityModel)model).cooldown;//+= Time.deltaTime/((FacilityModel)model).cooldown;
	
		}

		if (((FacilityView)view).progressSlider.value >= 1) {
			((FacilityView)view).progressSlider.gameObject.SetActive(false);
			((FacilityView)view).hammer.gameObject.SetActive(false);
			((FacilityView)view).facility.SetActive(true);
		}
	}

	public void StartBuildingTownFacility() 
	{
		GameData.instance.playerData.gold -= ((FacilityModel)model).cost;
		app.GetComponent<MainMenuView>().headerView.UpdateGoldValue();
		((FacilityModel)model).timeClicked = System.DateTime.Now;
		((FacilityView)view).hammer.gameObject.SetActive(true);
		((FacilityView)view).buildButton.gameObject.SetActive(false);
		((FacilityView)view).progressSlider.gameObject.SetActive(true);
		isBuilding = true;
	}
}
