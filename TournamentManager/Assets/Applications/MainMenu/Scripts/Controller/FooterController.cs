using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;
using System.Collections.Generic;

public class FooterController : Controller
{

	private List<Button> buttonList = new List<Button>();

	void Awake() {
		base.Awake();

		foreach (Button b in GetComponentsInChildren<Button>()) {
			buttonList.Add(b);
		}
	}

	public void DisableButtons() {
		for (int  i = 0; i < buttonList.Count; i++) {
			buttonList[i].interactable = false;
			buttonList[i].GetComponentInChildren<Text>().color = Color.gray;
		}
	}

	public void EnableButtons() {
		for (int  i = 0; i < buttonList.Count; i++) {
			buttonList[i].interactable = true;
			buttonList[i].GetComponentInChildren<Text>().color = Color.white;
		}
	}

}
