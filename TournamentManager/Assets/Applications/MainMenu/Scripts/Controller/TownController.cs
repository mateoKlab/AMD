using UnityEngine;
using System.Collections;
using Bingo;

public class TownController : Controller <MainMenu, TownModel, TownView>
{	
	public void CheckFacilityButtons() {
		foreach(FacilityController f in GetComponentsInChildren<FacilityController>()) 
		{
			f.CheckGold();
		}
	}
}
