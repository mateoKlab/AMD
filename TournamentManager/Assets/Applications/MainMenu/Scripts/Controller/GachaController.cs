using UnityEngine;
using System.Collections;
using Bingo;

public class GachaController : Controller
{
	public void CloseGachaPopUp(params object[] args) 
	{
		gameObject.SetActive(false);
		app.GetComponent<MainMenuView>().popUpShadeView.gameObject.SetActive(false);
		app.GetComponent<MainMenuController>().footerController.EnableButtons();
	}

	public void GenerateRandomCharacter() {
		if (GameData.instance.playerData.gold < 100) 
		{
			Debug.LogError ("Not enough gold.");
			return;
		}
		FighterData gachaCharacter = GameData.instance.fighterDatabase[(int)Random.Range(0, GameData.instance.fighterDatabase.Count)];

		bool isSuccessfullyAdded = GameData.instance.AddFighter(gachaCharacter);
		if(isSuccessfullyAdded)
		{
			app.GetComponent<MainMenuView>().gachaView.DisplayGachaCharacter(gachaCharacter);
			GameData.instance.playerData.gold -= 100;
			app.GetComponent<MainMenuView>().headerView.UpdateGoldValue();
			GameData.instance.playerData.Save ();
		}
		else
		{
			//TODO Popup something here
			Debug.LogError("Failed to add, team already full!");
		}
	}
}

