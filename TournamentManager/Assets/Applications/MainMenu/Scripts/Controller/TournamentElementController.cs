using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class TournamentElementController : Controller <MainMenu, TournamentElementModel, TournamentElementView>
{

	public Sprite emblem;
	public void SetRankValue(int i) {
		model.rankValue = i;
		view.rankLabel.text = i.ToString();
	}

	public void SetStageData(StageData sData) {
		emblem = Resources.Load<Sprite>("Sprites/Emblems/" + sData.id);
		view.fightButton.GetComponent<Image>().sprite = emblem;
		view.fightButton.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(emblem.texture.width * 1.7f,emblem.texture.height * 1.7f);
	
		model.stageData = sData;
		view.nameLabel.text = sData.name;
	}

	public void CheckIfStageIsUnlocked() {
		if (GameData.instance.playerData.tournamentProgress == app.model.tournamentModel.tournamentMatchList.IndexOf(model.stageData))
		{	
			view.frame.color = Color.white;
			view.fightButton.interactable = true;
			view.fightButton.GetComponent<Animator>().enabled = true;
			view.fightButton.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(emblem.texture.width * 1.8f,emblem.texture.height * 1.8f);
			view.OnClickDetailsButton();
		}
	}


}
