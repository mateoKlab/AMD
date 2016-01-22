using UnityEngine;
using System;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class EditEquipmentController : Controller <MainMenu, EditEquipmentModel, EditEquipmentView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
	/// 
	public FighterSpriteController fSpriteController;
	public GridLayoutGroup equipmentPanel;
	private GameObject equipmentPrefab;

	// Current Displayed EquipmentType;
	private Type currentType = typeof (EquipmentType);

	public void SetFighterToEdit(FighterData fData) 
	{
		gameObject.SetActive(true);
		model.fighterToEdit = fData;
		troopDetailsController.SetTroopDetails(fData);
	}
	
	public void OnEnable() 
	{
		LoadEquipment("EquipmentType+Weapon+Sword");
	}

	private void ClearEquipmentDisplay()
	{
		for (int i = 0; i < model.equipmentList.Count; i++)
		{
			DestroyObject(model.equipmentList[i].gameObject);
		}
	}

	public void LoadEquipment(string type) 
	{
		ClearEquipmentDisplay();
		equipmentPrefab = Resources.Load("Prefabs/EquipmentItemCell") as GameObject;
	

//		foreach (Type type in GameDatabase.equipmentDatabase.GetSubTypes) {
//
//			//Instantiate.
//		}

		foreach (Equipment eq in GameDatabase.equipmentDatabase.GetItems(Type.GetType(type))) 
		{
			GameObject go = Instantiate(equipmentPrefab);
				
			go.transform.SetParent(equipmentPanel.transform, false);
				
			EquipmentItemCellController eCon = go.GetComponent<EquipmentItemCellController>();
			model.equipmentList.Add (eCon);
			eCon.SetItemDetails(eq);
		}

		// Resize scrollable background based on number of elements
		float elementHeight = equipmentPrefab.GetComponent<LayoutElement>().preferredHeight + equipmentPanel.spacing.y;
		RectTransform rt = equipmentPanel.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.equipmentList.Count/3) + equipmentPanel.padding.top + equipmentPanel.padding.bottom);
	
	}

	public void CancelChangesToEquipment() 
	{
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
		app.view.editTeamView.gameObject.SetActive(true);
		app.controller.editTeamController.ShowTroopDetails(model.fighterToEdit);
	}

	public void SaveChangesToEquipment() 
	{
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
		app.view.editTeamView.gameObject.SetActive(true);
		app.controller.editTeamController.ShowTroopDetails(model.fighterToEdit);
	}

}
