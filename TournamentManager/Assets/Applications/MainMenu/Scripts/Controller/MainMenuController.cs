using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : Controller<MainMenu>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsController troopDetailsController { get; private set; }
    
    [Inject]
    public EditTeamController editTeamController { get; private set; }
    
    [Inject]
    public TournamentController tournamentController { get; private set; }
    
    [Inject]
    public ArmoryController armoryController { get; private set; }
    
    [Inject]
    public GachaController gachaController { get; private set; }
    
    [Inject]
    public TownController townController { get; private set; }
    
    [Inject]
    public MissionController missionController { get; private set; }
    
    [Inject]
    public FooterController footerController { get; private set; }
    
    [Inject]
    public HeaderController headerController { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////

    public override void Awake()
    {
        base.Awake();

        Messenger.AddListener(MainMenuEvents.START_BATTLE, GoToBattleScene);
        Messenger.AddListener(MainMenuEvents.CLOSE_POPUP, ClosePopUp);
		Messenger.AddListener(MainMenuEvents.SHOW_MENU, ShowMenu);
		Messenger.AddListener(MainMenuEvents.HIDE_MENU, HideMenu);
    }
	
	public void Update() {
		if(Input.GetMouseButtonDown(0))
		{
			PointerEventData pointer = new PointerEventData(EventSystem.current);
			pointer.position = Input.mousePosition;
			
			List<RaycastResult> raycastResults = new List<RaycastResult>();
			EventSystem.current.RaycastAll(pointer, raycastResults);
			
			if(raycastResults.Count > 0)
			{
				if(raycastResults[0].gameObject.tag != "MenuItem")
					//Messenger.Send(MainMenuEvents.HIDE_MENU);
					HideMenu();
			}
		}

	}

    public void GoToBattleScene(params object[] args)
    {
        Application.LoadLevel("BattleScene");
    }
    
    public void ShowStablePopUp(params object[] args)
    {
		HideMenu();
        editTeamController.ShowEditTeam();
    }

	public void ShowMenu(params object[] args)
	{
		footerController.GetComponent<Animator>().ResetTrigger("HideMenu");
		footerController.GetComponent<Animator>().SetTrigger("ShowMenu");
	}

	public void HideMenu(params object[] args)
	{
		footerController.GetComponent<Animator>().ResetTrigger("ShowMenu");
		footerController.GetComponent<Animator>().SetTrigger("HideMenu");
	}

    public void ShowTournamentPopUp(params object[] args)
    {
        //footerController.DisableButtons();
        GetComponent<MainMenuView>().tournamentView.gameObject.SetActive(true);
		app.controller.tournamentController.GetComponent<Animator>().SetTrigger("TransitionIn");
    }

    public void ShowTownPopUp(params object[] args)
    {
        //footerController.DisableButtons();
		HideMenu();
        GetComponent<MainMenuView>().townView.gameObject.SetActive(true);
    }

    public void ShowGachaPopUp(params object[] args)
    {
        //footerController.DisableButtons();
		HideMenu();
        GetComponent<MainMenuView>().gachaView.gameObject.SetActive(true);
    }

	public void ShowArmoryPopUp(params object[] args)
	{
		//footerController.DisableButtons();
		HideMenu();
		if (GetComponent<MainMenuView>().gachaView.gameObject.activeSelf)
		{
			app.view.isShowingGachaPopUp = true;
			GetComponent<MainMenuView>().gachaView.gameObject.SetActive(false);
		} else {
			app.view.isShowingGachaPopUp = false;
		}

		GetComponent<MainMenuView>().armoryView.gameObject.SetActive(true);
	}

    public void EnableMainMenuItems(bool enabled)
    {
        if (enabled)
        {
            footerController.EnableButtons();
        }
        else
        {
            footerController.DisableButtons();
        }
    }

    public void ClosePopUp(params object[] args)
    {
        GameObject popUp = args[0] as GameObject;
        popUp.SetActive(false);
        EnableMainMenuItems(true);
    }
}

