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

//        FighterExpController[] fighters = GetComponentsInChildren<FighterExpController>(true);
//        model.fighters = new System.Collections.Generic.List<FighterExpController>(fighters);
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

		foreach (FighterData fighter in GameData.instance.GetActiveParty()) 
		{
			model.fighters[i].EnableFighter(true);
			model.fighters[i].SetFighterDetails(fighter);
			i++;
		}
//        for(int j = 0; j < gameData.GetActiveParty().Count; j++)
//        {
//            FighterData fd = gameData.GetActiveParty().IndexOf(j);
//            if(fd != null)
//            {
//                model.fighters[i].EnableFighter(true);
//                model.fighters[i].SetFighterDetails(fd);
//                i++;
//            }
//        }

		view.expLabel.text = gameData.currentStage.xpReward.ToString();
		StartCoroutine(AddExpToFighters(gameData.currentStage.xpReward));
    }

    public void OnReturnToMainMenu(params object[] args)
    {
		StartCoroutine(ReturnToMainMenuCoroutine());
    }

	IEnumerator ReturnToMainMenuCoroutine() {
		app.GetComponentInChildren<Animator>().SetTrigger("HideScroll");
		yield return new WaitForSeconds(1);
		Application.LoadLevel("MainMenuScene");
	}

	IEnumerator AddExpToFighters(int expEarned) {
		int exp = expEarned;
		while (exp > 0) 
		{
			exp -= 10;
			view.expLabel.text = exp.ToString();

			for(int i = 0; i < model.fighters.Count; i++) {
				model.fighters[i].UpdateExpMeter(expEarned - exp);
			}

			yield return new WaitForEndOfFrame();
		}

		view.expLabel.text = "0";
	}

}           
