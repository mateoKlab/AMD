using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleEndExpView : View
{
	public Text expLabel;
	private Button mainMenuButton;

    public override void Awake()
    {
        base.Awake();

        mainMenuButton = transform.Find("MainMenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => OnClickMainMenu());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnClickMainMenu()
    {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
        Messenger.Send(EventTags.RETURN_TO_MAIN_MENU);
    }
}
