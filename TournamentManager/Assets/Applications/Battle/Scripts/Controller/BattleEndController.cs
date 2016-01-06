using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class BattleEndController : Controller <Battle, BattleEndModel, BattleEndView>
{
	private bool isLerping;
	
	void Update() {
		if (Input.GetMouseButtonDown(0)) 
		{
			isLerping = false;
		}
	}

	public void ShowBattleEndPopUp(bool didWin) {
		view.gameObject.SetActive(true);

		if (didWin) 
		{
			view.headerLabel.text = "VICTORY";
			GetComponent<Animator>().SetTrigger("WinTransitionIn");
			StartCoroutine(LerpRewardValuesCoroutine());
		}
		else
		{
			view.headerLabel.text = "DEFEAT";
			GetComponent<Animator>().SetTrigger("LoseTransitionIn");
			model.gold = 0;
			model.exp = 0;
		}
	}

	public void ShowExpScreen() 
	{
		//Application.LoadLevel("MainMenuScene");
        Messenger.Send(EventTags.END_SCREEN_EXP);
        gameObject.SetActive(false);
	}

	public void ReturnToMainMenu() 
	{
		Application.LoadLevel("MainMenuScene");
	}

	IEnumerator LerpRewardValuesCoroutine() {
		isLerping = true;
		yield return new WaitForSeconds(1);
		int tempGold = 0;
		int tempExp = 0;
		while((tempGold < model.gold || tempExp < model.exp) && isLerping) {
			if (tempGold < model.gold) {
				tempGold += 50;
			}
			if (tempExp < model.exp) {
				tempExp += 50;
			}
			view.goldValue.text = tempGold.ToString();
			view.expValue.text = tempExp.ToString();
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}
		view.goldValue.text = model.gold.ToString();
		view.expValue.text = model.exp.ToString();
		view.continueButton.SetActive(true);

	
		float i = 0;
		while (i < 1)
		{
			view.continueButton.GetComponent<RectTransform>().localScale = Vector3.one * i/1;
			i += Time.fixedDeltaTime * 10;
			yield return new WaitForEndOfFrame();
		}

		GameData.instance.playerData.gold += model.gold;
		GameData.instance.SavePlayerData();
	}
}
