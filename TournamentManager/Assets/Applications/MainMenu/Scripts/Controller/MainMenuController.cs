using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuController : Controller<MainMenu, MainMenuModel, MainMenuView>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public EditEquipmentController editEquipmentController { get; private set; }
    
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
	
	private bool menuOn = false;

    public override void Awake()
    {
        base.Awake();

        //Messenger.AddListener(MainMenuEvents.START_BATTLE, GoToBattleScene);
        Messenger.AddListener(MainMenuEvents.CLOSE_POPUP, ClosePopUp);
		//Messenger.AddListener(MainMenuEvents.SHOW_MENU, ShowMenu);
		//Messenger.AddListener(MainMenuEvents.HIDE_MENU, HideMenu);

    }

	public void Start() {
		SoundManager.instance.PlayBGM("Audio/BGM/SampleBGM");
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
		StartCoroutine(TransitionToBattleSceneCoroutine());
    }
    
	IEnumerator TransitionToBattleSceneCoroutine() {
		SoundManager.instance.FadeOutBGM(0.1f);
		SoundManager.instance.PlaySFX("Audio/SFX/Drums/Drum3");
		view.fadeMask.gameObject.SetActive(true);
		yield return new WaitForSeconds(1f);
		GetComponentInChildren<Animator>().SetTrigger("TransitionOut");
		yield return new WaitForSeconds(1f);

		Application.LoadLevel("BattleScene");
	}

    public void ShowStablePopUp(params object[] args)
    {
		HideMenu();
        editTeamController.ShowEditTeam();
    }

	public void ShowMenu(params object[] args)
	{
		if(!menuOn) {
			menuOn = true;
			SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
			footerController.GetComponent<Animator>().ResetTrigger("HideMenu");
			footerController.GetComponent<Animator>().SetTrigger("ShowMenu");
		}

	}

	public void HideMenu(params object[] args)
	{
		if (menuOn) 
		{
			menuOn = false;
			SoundManager.instance.PlaySFX("Audio/SFX/Card_Flip");
			footerController.GetComponent<Animator>().ResetTrigger("ShowMenu");
			footerController.GetComponent<Animator>().SetTrigger("HideMenu");
		}
	
	}

    public void ShowTournamentPopUp(params object[] args)
    {
        //footerController.DisableButtons();
		SoundManager.instance.PlayUISFX("Audio/SFX/Button2");
        view.tournamentView.gameObject.SetActive(true);
		tournamentController.GetComponent<Animator>().SetTrigger("TransitionIn");
    }

    public void ShowTownPopUp(params object[] args)
    {
        //footerController.DisableButtons();
		HideMenu();
        view.townView.gameObject.SetActive(true);
    }

    public void ShowGachaPopUp(params object[] args)
    {
        //footerController.DisableButtons();
		HideMenu();
		gachaController.TransitionToGachaScreen();
    }

	public void ShowArmoryPopUp(params object[] args)
	{
		//footerController.DisableButtons();
		HideMenu();
		if (view.gachaView.gameObject.activeSelf)
		{
			app.view.isShowingGachaPopUp = true;
			view.gachaView.gameObject.SetActive(false);
		} else {
			view.isShowingGachaPopUp = false;
		}

		view.armoryView.gameObject.SetActive(true);
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

