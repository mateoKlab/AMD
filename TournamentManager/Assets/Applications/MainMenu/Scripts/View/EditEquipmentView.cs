using UnityEngine;
using System.Collections;
using Bingo;

public class EditEquipmentView : View <MainMenu, EditEquipmentModel, EditEquipmentController>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public TroopDetailsView troopDetailsView { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public void OnClickCloseButton() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		Messenger.Send(MainMenuEvents.CLOSE_POPUP, this.gameObject);
		app.GetComponent<MainMenuView>().editTeamView.gameObject.SetActive(true);
	}
}
