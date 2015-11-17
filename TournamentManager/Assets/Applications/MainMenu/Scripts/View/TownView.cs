using UnityEngine;
using System.Collections;
using Bingo;

public class TownView : View
{
	public void OnClickCloseButton() 
	{
		((TownController)controller).CloseTownPopUp();
	}
}
