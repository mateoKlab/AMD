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
		StartCoroutine(ReturnToMainMenuCoroutine());
	}

	IEnumerator ReturnToMainMenuCoroutine() {
		app.GetComponentInChildren<Animator>().SetTrigger("FadeOut");
		yield return new WaitForSeconds(0.3f);
		Application.LoadLevel("MainMenuScene");
	}
	
	IEnumerator LerpRewardValuesCoroutine() {
		isLerping = true;
		yield return new WaitForSeconds(1);
		int tempGold = 0;
		int tempExp = 0;
		float time = 0;
	
		while((tempGold < model.gold || tempExp < model.exp) && isLerping && time < 1f) {
			if (tempGold < model.gold) {
				tempGold += model.gold/50;
			}
			if (tempExp < model.exp) {
				tempExp += model.gold/50;
			}
			view.goldValue.text = tempGold.ToString();
			view.expValue.text = tempExp.ToString();
	
			time += Time.fixedDeltaTime;
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}

		view.goldValue.text = model.gold.ToString();
		view.expValue.text = model.exp.ToString();
		view.continueButton.SetActive(true);
		iTween.PunchScale(view.continueButton, Vector3.one * 1.2f, 0.7f);

	
		GameData.instance.playerData.gold += model.gold;
		GameData.instance.SavePlayerData();
	}
}
