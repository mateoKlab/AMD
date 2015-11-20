using UnityEngine;
using System.Collections;
using Bingo;

public class GachaController : Controller <MainMenu, GachaModel, GachaView>
{

	public void GenerateRandomCharacter() {
		if (GameData.instance.playerData.gold < model.rollCost)
		{
			Debug.LogError ("Not enough gold.");
			return;
		}
		FighterData gachaCharacter = new FighterData();//GameData.instance.fighterDatabase[(int)Random.Range(0, GameData.instance.fighterDatabase.Count)];

		gachaCharacter.name = model.namePool[(int)Random.Range(0, model.namePool.Count)];
		gachaCharacter.fighterElement = (FighterElement)Random.Range(0, 4);
		gachaCharacter.HP = (int)Random.Range(5, 15) * 100; 
		gachaCharacter.ATK = (int)Random.Range(5, 15) * 10;
		gachaCharacter.isRanged = System.Convert.ToBoolean (Random.Range (0, 2)); // Random between 0 and 1. Max value is exclusive.
		gachaCharacter.fighterClass = model.classPool[(int)Random.Range(0, model.classPool.Count)];

		bool isSuccessfullyAdded = GameData.instance.AddFighter(gachaCharacter);
		if(isSuccessfullyAdded)
		{
			view.DisplayGachaCharacter(gachaCharacter);
			GameData.instance.playerData.gold -= model.rollCost;
			app.view.headerView.UpdateGoldValue();
			GameData.instance.playerData.Save ();
			CheckGold();
		}
		else
		{
			//TODO Popup something here
			Debug.LogError("Failed to add, team already full!");
		}
	}

	public void CheckGold() {
		if (GameData.instance.playerData.gold < model.rollCost)
		{
			view.rollButton.interactable = false;
		}
	}
}

