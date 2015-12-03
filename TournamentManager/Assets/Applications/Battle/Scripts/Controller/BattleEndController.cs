using UnityEngine;
using System.Collections;
using Bingo;

public class BattleEndController : Controller <Battle, BattleEndModel, BattleEndView>
{
	public void ShowBattleEndPopUp(bool didWin) {
		view.gameObject.SetActive(true);

		if (didWin) 
		{
			view.headerLabel.text = "Victory!";
		}
		else
		{
			view.headerLabel.text = "Defeat!";
			model.gold = 0;
			model.exp = 0;
		}

		view.goldValue.text = model.gold.ToString();
		view.expValue.text = model.exp.ToString();

		GameData.instance.playerData.gold += model.gold;
		GameData.instance.SavePlayerData();
	}

	public void ReturnToMainMenu() 
	{
		//Application.LoadLevel("MainMenuScene");
        Messenger.Send(EventTags.END_SCREEN_EXP);
        gameObject.SetActive(false);
	}
}
