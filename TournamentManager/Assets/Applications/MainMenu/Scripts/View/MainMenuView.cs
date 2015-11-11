using UnityEngine;
using System.Collections;
using Bingo;

public class MainMenuView : View<MainMenu>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TournamentView tournamentView { get; private set; }
    
    [Inject]
    public EditTeamCloseButtonView editTeamCloseButtonView { get; private set; }
    
    [Inject]
    public EditTeamView editTeamView { get; private set; }
    
    [Inject]
    public GachaView gachaView { get; private set; }
    
    [Inject]
    public TownView townView { get; private set; }
    
    [Inject]
    public PopUpShadeView popUpShadeView { get; private set; }
    
    [Inject]
    public QuestView questView { get; private set; }
    
    [Inject]
    public FooterView footerView { get; private set; }
    
    [Inject]
    public HeaderView headerView { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public void OnClickBattleButton() {
		((MainMenuController)controller).GoToBattleScene();
	}

	public void OnClickTournamentButton() {
		((MainMenuController)controller).ShowTournamentPopUp();
	}

	public void OnClickTownButton() {
		((MainMenuController)controller).ShowTownPopUp();
	}

	public void OnClickStableButton() {
		((MainMenuController)controller).ShowStablePopUp();
	}

	public void OnClickGachaButton() {
		((MainMenuController)controller).ShowGachaPopUp();
	}
}

