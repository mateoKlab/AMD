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

	public void TransitionToGachaScreen() {
		gameObject.SetActive(true);
		GetComponent<Animator>().SetTrigger("TransitionIn");
	}

	public void CloseGachaScreen() {
		StartCoroutine(CloseGachaScreenCoroutine());
	}

	IEnumerator CloseGachaScreenCoroutine() {
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}
}

