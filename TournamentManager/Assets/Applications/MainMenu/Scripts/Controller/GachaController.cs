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
		FighterData gachaCharacter = new FighterData();
		gachaCharacter.HP = (int) (gachaCharacter.HP * (1 + Random.value)); 
		gachaCharacter.ATK = (int) (gachaCharacter.ATK * (1 + Random.value)); 
		GameData.Instance.PlayerData.fightersOwned.Add(gachaCharacter);

		app.GetComponent<MainMenuView>().gachaView.DisplayGachaCharacter(gachaCharacter.HP, gachaCharacter.ATK);
	}
}
