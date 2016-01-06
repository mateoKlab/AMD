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
		((BattleEndController)controller).ShowExpScreen();
	}

	public void OnClickReturnButton() 
	{
		((BattleEndController)controller).ReturnToMainMenu();
	}
}
