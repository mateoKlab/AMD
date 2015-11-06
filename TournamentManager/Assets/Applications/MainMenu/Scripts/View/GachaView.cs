using UnityEngine;
using System.Collections;
using Bingo;

public class GachaView : View
{
	public void OnClickCloseButton() {
		((GachaController)controller).CloseGachaPopUp();
	}
}
