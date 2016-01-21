using UnityEngine;
using System.Collections;
using Bingo;

public class EditEquipmentController : Controller <MainMenu, EditEquipmentModel, EditEquipmentView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public void SetFighterToEdit(FighterData fData) 
	{
		gameObject.SetActive(true);
		model.fighterToEdit = fData;
		troopDetailsController.SetTroopDetails(fData);
	}
	
}
