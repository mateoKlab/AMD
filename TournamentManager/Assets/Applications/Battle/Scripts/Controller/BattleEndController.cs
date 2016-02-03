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
		DamageManager.instance.gameObject.SetActive(false);
		view.gameObject.SetActive(true);

		if (didWin) 
		{
			//SoundManager.instance.FadeOutBGM(0.1f);
			SoundManager.instance.StopBGM();
			//SoundManager.instance.PlayUISFX("Audio/SFX/Gong");

			view.victoryBanner.SetActive(true);
			view.defeatBanner.SetActive(false);
			GetComponent<Animator>().SetTrigger("WinTransitionIn");
			StartCoroutine(LerpRewardValuesCoroutine());
		}
		else
		{
			SoundManager.instance.FadeOutBGM(0.1f);
			SoundManager.instance.PlayUISFX("Audio/SFX/Drums/Drum2");
			view.defeatBanner.SetActive(true);
			view.victoryBanner.SetActive(false);
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
		yield return new WaitForSeconds(1);
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
			SoundManager.instance.PlaySFX("Audio/SFX/Cash");
			yield return new WaitForSeconds(Time.fixedDeltaTime);
		}

		view.goldValue.text = model.gold.ToString();
		view.expValue.text = model.exp.ToString();
		view.continueButton.SetActive(true);
		iTween.PunchScale(view.continueButton, Vector3.one * .2f, 0.7f);
		yield return new WaitForSeconds(0.3f);
		SoundManager.instance.PlayUISFX("Audio/SFX/Card_Flip");
	
		GameData.instance.playerData.gold += model.gold;
		GameData.instance.SavePlayerData();
	}

	public void PlayBGMEvent() {
		SoundManager.instance.PlayBGM("Audio/BGM/WinTheme");
	}

	public void PlaySFXEvent(string sfxName) {
		SoundManager.instance.PlaySFX("Audio/SFX/" + sfxName);
	}
	public void PlayUISFXEvent(string sfxName) {
		SoundManager.instance.PlayUISFX("Audio/SFX/" + sfxName);
	}
}
