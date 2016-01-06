using UnityEngine;
using System.Collections;
using Bingo;

public class FooterView : View <MainMenu>
{
	public void OnClickMenuButton() {
		//Messenger.Send(MainMenuEvents.SHOW_MENU);
		app.controller.ShowMenu();
	}
}
