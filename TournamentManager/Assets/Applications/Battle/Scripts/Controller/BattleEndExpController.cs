using UnityEngine;
using System.Collections;
using Bingo;

public class BattleEndExpController : Controller<Battle, BattleEndExpModel, BattleEndExpView>
{
    private GameData gameData;

    public override void Awake()
    {
        base.Awake();

        gameData = GameData.instance;

        FighterExpController[] fighters = GetComponentsInChildren<FighterExpController>(true);
        model.fighters = new System.Collections.Generic.List<FighterExpController>(fighters);
        Messenger.AddListener(EventTags.RETURN_TO_MAIN_MENU, OnReturnToMainMenu);
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(EventTags.RETURN_TO_MAIN_MENU, OnReturnToMainMenu);
    }

    public void Show()
    {
        this.gameObject.SetActive(true);

        int i = 0;
        for(int j = 0; j < gameData.GetActiveParty().Length; j++)
        {
            FighterData fd = gameData.GetActiveFighter(j);
            if(fd != null)
            {
                model.fighters[i].EnableFighter(true);
                model.fighters[i].SetFighterDetails(fd);
                i++;
            }
        }

        StartCoroutine("AnimateExpSliders");
    }

    private IEnumerator AnimateExpSliders()
    {
        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < model.fighters.Count; i++)
        {
            if(model.fighters[i].gameObject.activeSelf)
            {
                model.fighters[i].AnimateExpSlider();
            }
        }
    }

    public void OnReturnToMainMenu(params object[] args)
    {
        Application.LoadLevel("MainMenuScene");
    }

}           
