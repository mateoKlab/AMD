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
//	public FighterSpriteController fSpriteController;

	//private FighterSpriteController mageSprite;
	//private FighterSpriteController warriorSprite;

	private Class currentFighterClass;

	public GridLayoutGroup equipmentPanel;
	public GameObject warriorTabs;
	public GameObject mageTabs;
	private GameObject equipmentPrefab;

	// Current Displayed EquipmentType;
	private Type currentType = typeof (EquipmentType);

	public void SetFighterToEdit(FighterData fData) 
	{
		gameObject.SetActive(true);
		GetComponent<Animator>().SetTrigger("TransitionIn");

		model.fighterToEdit = fData;
		troopDetailsController.SetTroopDetails(fData);
		if (fData.fighterClass == Class.Warrior) 
		{
			warriorTabs.SetActive(true);
			mageTabs.SetActive(false);
			LoadEquipment("EquipmentType+Weapon+Sword");
		}
		else 
		{
			mageTabs.SetActive(true);
			warriorTabs.SetActive(false);
			LoadEquipment("EquipmentType+Weapon+Staff");
		}
		currentFighterClass = fData.fighterClass;
	}

	private void ClearEquipmentDisplay()
	{
		for (int i = 0; i < model.equipmentList.Count; i++)
		{
			DestroyObject(model.equipmentList[i].gameObject);
		}
	}

	public void SetFighterSkin (FighterSkinData skinData)
	{
		troopDetailsController.view.warriorSprite.gameObject.SetActive (false);
		troopDetailsController.view.mageSprite.gameObject.SetActive (false);

		switch (currentFighterClass) {
		case Class.Warrior:
		{
			troopDetailsController.view.warriorSprite.SetFighterSkin (skinData);
			troopDetailsController.view.warriorSprite.gameObject.SetActive (true);
			break;
		}

		case Class.Mage:
		{
			troopDetailsController.view.mageSprite.SetFighterSkin (skinData);
			troopDetailsController.view.mageSprite.gameObject.SetActive (true);
			break;
		}
		

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
		// Add function to revert changes
		StartCoroutine(TransitionToEditTeam());
	}

	public void SaveChangesToEquipment() 
	{
		StartCoroutine(TransitionToEditTeam());
	}

	IEnumerator TransitionToEditTeam() {
		GetComponent<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(0.5f);
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
		app.view.editTeamView.gameObject.SetActive(true);
		GetComponent<Animator>().ResetTrigger("TransitionOut");
		app.view.editTeamView.gameObject.GetComponent<Animator>().SetTrigger("TransitionFromEquips");
		app.controller.editTeamController.ShowTroopDetails(model.fighterToEdit);
	}

}
