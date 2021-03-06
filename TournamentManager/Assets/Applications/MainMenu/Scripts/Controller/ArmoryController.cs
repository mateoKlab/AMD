﻿using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArmoryController : Controller <MainMenu, ArmoryModel, ArmoryView>
{
	private GridLayoutGroup content;
	private GameObject tempHolder;
	private float elementHeight;
	private List<GameObject> elementsList = new List<GameObject>();

	public void Start()
	{
		tempHolder = new GameObject("TempItemHolder");
		tempHolder.transform.SetParent(this.transform, false);
		
		InitializeScrollView();
		DisplayWeapons();
	}

	public void InitializeScrollView() 
	{
		content = transform.FindChild("ArmoryScrollView").GetComponentInChildren<GridLayoutGroup>();
		GameObject armoryElement;
		
		for (int i = 0; i < model.weaponList.Count; i++) {		
			armoryElement = Instantiate(Resources.Load("Prefabs/ArmoryItem", typeof(GameObject))) as GameObject;	
			elementsList.Add(armoryElement);
			elementHeight = armoryElement.GetComponent<LayoutElement>().preferredHeight + content.spacing.y;
		}
		
		for (int i = 0; i < elementsList.Count; i++) {
			elementsList[i].transform.SetParent(content.transform, false);
		}
		
		// Resize scrollable background based on number of elements
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.weaponList.Count/5) + content.padding.top + content.padding.bottom);
	}

	public void DisplayWeapons() 
	{
		ClearItems();

		for(int i = 0; i < model.weaponList.Count; i++) 
		{
			elementsList[i].SetActive(true);
			elementsList[i].GetComponent<ArmoryItemController>().DisplayArmoryItem(model.weaponList[i], "Weapon");
			elementsList[i].transform.SetParent(content.transform, false);
		}
		
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.weaponList.Count/5) + content.padding.top + content.padding.bottom);
	}

	public void DisplayArmors()
	{
		ClearItems();

		for(int i = 0; i < model.armorList.Count; i++) 
		{
			elementsList[i].SetActive(true);
			elementsList[i].GetComponent<ArmoryItemController>().DisplayArmoryItem(model.armorList[i], "Body");
			elementsList[i].transform.SetParent(content.transform, false);
		}
		
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.armorList.Count/5) + content.padding.top + content.padding.bottom);
	}

	public void DisplayItems(List<Equipment> itemList)
	{
		for(int i = 0; i < model.armorList.Count; i++) 
		{
			elementsList[i].SetActive(true);
			elementsList[i].GetComponent<ArmoryItemController>().DisplayArmoryItem(itemList[i], itemList[i].type.ToString());
			elementsList[i].transform.SetParent(content.transform, false);
		}
		
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * Mathf.Ceil((float)model.armorList.Count/5) + content.padding.top + content.padding.bottom);
	}


	public void ClearItems() 
	{
		for(int i = 0; i < elementsList.Count; i++) 
		{
			elementsList[i].SetActive(false);
			elementsList[i].transform.SetParent(tempHolder.transform, false);
		}
	}
	
//	public void HideArmors() 
//	{
//		for(int i = 0; i < model.armorList.Count; i++) 
//		{
//			elementsList[i].SetActive(false);
//			elementsList[i].transform.SetParent(tempHolder.transform, false);
//		}
//	}
}
