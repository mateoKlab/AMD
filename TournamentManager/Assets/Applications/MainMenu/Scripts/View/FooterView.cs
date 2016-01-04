using UnityEngine;
using System.Collections;
using Bingo;

public class FooterView : View
{
	public void OnClickMenuButton() {
		Messenger.Send(MainMenuEvents.SHOW_MENU);
	}
}
