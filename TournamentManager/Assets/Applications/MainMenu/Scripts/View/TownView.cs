using UnityEngine;
using System.Collections;
using Bingo;

public class TownView : View
{
	public void OnClickCloseButton() 
	{
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
	}
}
