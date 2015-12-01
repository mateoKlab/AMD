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
		FighterData gachaCharacter = FighterGenerator.GenerateFighter ();

		bool isSuccessfullyAdded = GameData.instance.AddFighter(gachaCharacter);
		if(isSuccessfullyAdded)
		{
			// TODO: Build/Randomize sprite here, then save to playerData.

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

