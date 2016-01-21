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
	
	public void OnEnable() {
	
		LoadEquipment("EquipmentType+Weapon+Sword");
	}

//	public void OnClickCell (Type type)
//	{
//		LoadEquipment ();
//		currentType = type;
//	}

	public void OnClickCell (Equipment equipment)
	{
		//Equip.
	}

	private void ClearEquipmentDisplay()
	{
		for (int i = 0; i < model.equipmentList.Count; i++)
		{
			DestroyObject(model.equipmentList[i].gameObject);
		}
	}

	public void LoadEquipment(string type) {
		ClearEquipmentDisplay();
		equipmentPrefab = Resources.Load("Prefabs/EquipmentItemCell") as GameObject;
	

//		foreach (Type type in GameDatabase.equipmentDatabase.GetSubTypes) {
//
//			//Instantiate.
//		}
		foreach (Equipment eq in GameDatabase.equipmentDatabase.GetItems(Type.GetType(type))) {
			// INstantiate.

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
}
