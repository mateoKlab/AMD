using UnityEngine;
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

	public void SetFighterToEdit(FighterData fData) 
	{
		gameObject.SetActive(true);
		model.fighterToEdit = fData;
		troopDetailsController.SetTroopDetails(fData);
	}
	
	public void OnEnable() {
		ClearEquipmentDisplay();
		LoadEquipment();
	}

	private void ClearEquipmentDisplay()
	{
		for (int i = 0; i < model.equipmentList.Count; i++)
		{
			DestroyObject(model.equipmentList[i].gameObject);
		}
	}

	public void LoadEquipment() {
		equipmentPrefab = Resources.Load("Prefabs/Equipment") as GameObject;
	
		for (int i = 0 ; i < 8; i++) // 8 = number of branches
		{
			GameObject go = Instantiate(equipmentPrefab);

			go.transform.SetParent(equipmentPanel.transform, false);
			
			EquipmentCellController eCon = go.GetComponent<EquipmentCellController>();
			model.equipmentList.Add (eCon);
		}
		
		// Resize scrollable background based on number of elements
		float elementHeight = equipmentPrefab.GetComponent<LayoutElement>().preferredHeight + equipmentPanel.spacing.y;
		RectTransform rt = equipmentPanel.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.equipmentList.Count/3) + equipmentPanel.padding.top + equipmentPanel.padding.bottom);
	
	}
}
