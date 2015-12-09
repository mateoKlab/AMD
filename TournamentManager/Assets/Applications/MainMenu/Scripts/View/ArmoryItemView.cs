using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;
public class ArmoryItemView : View
{
	public RawImage itemSprite;
	public Text itemLabel;
	public GameObject diamondsIcon;

	public void OnClickArmoryItem() {
		((ArmoryItemController)controller).UnlockItem();
	}
}
