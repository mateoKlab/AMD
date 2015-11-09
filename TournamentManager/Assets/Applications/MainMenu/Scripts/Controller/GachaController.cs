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

		Debug.Log ( GameData.Instance.fighterDatabase.Count);
		FighterData gachaCharacter = GameData.Instance.fighterDatabase[0];//GameData.Instance.fighterDatabase[(int)Random.Range(0, GameData.Instance.fighterDatabase.Count)];
	
		GameData.Instance.PlayerData.fightersOwned.Add(gachaCharacter);

		app.GetComponent<MainMenuView>().gachaView.DisplayGachaCharacter(gachaCharacter.name, gachaCharacter.HP, gachaCharacter.ATK);

		GameData.Instance.PlayerData.Save ();
	}
}

