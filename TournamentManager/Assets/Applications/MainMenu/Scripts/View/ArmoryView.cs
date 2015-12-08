using UnityEngine;
using System.Collections;
using Bingo;

public class ArmoryView : View
{

	public void OnClickWeaponsTab() 
	{
		((ArmoryController)controller).DisplayWeapons();
	}

	public void OnClickArmorsTab()
	{
		((ArmoryController)controller).DisplayArmors();
	}

	public void OnClickCloseButton() {
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);

		if(app.GetComponent<MainMenuView>().isShowingGachaPopUp) 
		{
			app.GetComponent<MainMenuView>().gachaView.gameObject.SetActive(true);
		}
	}
}
