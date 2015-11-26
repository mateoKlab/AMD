using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class BattleEndView : View
{
	public Text headerLabel;
	public Text expValue;
	public Text goldValue;

	public void OnClickReturnButton() 
	{
		((BattleEndController)controller).ReturnToMainMenu();
	}
}
