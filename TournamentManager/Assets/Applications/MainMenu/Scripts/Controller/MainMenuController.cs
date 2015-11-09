using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuController : Controller<MainMenu>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public QuestController questController { get; private set; }
    
    [Inject]
    public GachaController gachaController { get; private set; }
    
    [Inject]
    public TownController townController { get; private set; }
    
    [Inject]
    public FooterController footerController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////

	public override void Awake() 
	{
		base.Awake();
		PopulateQuestList();
	}
	
	private void PopulateQuestList() 
	{
		// HACK Test code
		Debug.Log ("PopulateQuestList");

		foreach (Button qPoint in app.model.questModel.transform.GetComponentsInChildren<Button>())
		{
			app.model.questModel.questPoints.Add(qPoint.gameObject);
		}

		StageData testData = new StageData();

		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 2000;
		testFighter1.ATK = 50;
		
		testData.enemies.Add (testFighter1);
		app.model.questModel.questList.Add(testData);

		testData.enemies.Add (testFighter1);
		app.model.questModel.questList.Add(testData);

	}

	public void GoToBattleScene(params object[] args) 
	{
		Application.LoadLevel("BattleScene");
	}

	public void ShowTownPopUp(params object[] args) 
	{
		footerController.DisableButtons();
		app.view.popUpShadeView.gameObject.SetActive(true);
		app.view.townView.gameObject.SetActive(true);
	}

	public void ShowStablePopUp(params object[] args)
	{
		Debug.LogError("Stable");
	}

	public void ShowGachaPopUp(params object[] args) 
	{
		footerController.DisableButtons();
		app.view.popUpShadeView.gameObject.SetActive(true);
		app.view.gachaView.gameObject.SetActive(true);
	}
}

