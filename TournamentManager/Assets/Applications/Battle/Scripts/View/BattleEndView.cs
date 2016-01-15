using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class BattleEndView : View
{
	public Text headerLabel;
	public Text expValue;
	public Text goldValue;
	public GameObject continueButton;

	public void OnClickNextButton() 
	{
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		((BattleEndController)controller).ShowExpScreen();
	}

	public void OnClickReturnButton() 
	{
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		((BattleEndController)controller).ReturnToMainMenu();
	}

	public void OnClickReviveButton() 
	{
		SoundManager.instance.PlayUISFX("Audio/SFX/ButtonDisabled");
	}
}
