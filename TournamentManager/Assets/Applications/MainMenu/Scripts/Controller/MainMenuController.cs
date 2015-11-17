using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;

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
    }

    public void GoToBattleScene(params object[] args)
    {
        Application.LoadLevel("BattleScene");
    }
    
    public void ShowStablePopUp(params object[] args)
    {
        EnableMainMenuItems(false);
        editTeamController.ShowEditTeam();
    }

    public void ShowTournamentPopUp(params object[] args)
    {
        footerController.DisableButtons();
        GetComponent<MainMenuView>().tournamentView.gameObject.SetActive(true);
    }

    public void ShowTownPopUp(params object[] args)
    {
        footerController.DisableButtons();
        GetComponent<MainMenuView>().townView.gameObject.SetActive(true);
    }

    public void ShowGachaPopUp(params object[] args)
    {
        footerController.DisableButtons();
        GetComponent<MainMenuView>().gachaView.gameObject.SetActive(true);
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

