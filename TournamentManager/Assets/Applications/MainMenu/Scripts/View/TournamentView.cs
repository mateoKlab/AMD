using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;
using System.Collections.Generic;

public class TournamentView : View
{
	public HorizontalLayoutGroup content;

	private float elementWidth;
	private List<GameObject> elementsList = new List<GameObject>();

	public GameObject stageDetailsPopUp;
	public Text stageNameLabel;
	public Text enemyCountLabel;
	public Text goldRewardLabel;
	public Text expRewardLabel;
	//public RawImage emblem;

	public void Start()
	{
		//base.Start();

		InitializeScrollView();
	}

	public void OnClickCloseButton() {
		SoundManager.instance.PlayUISFX("Audio/SFX/Button1");
		((TournamentController)controller).TransitionOut();
	}

//	public void OnClickTournamentButton(string id) {
//		GameData.instance.currentStage = ((TournamentModel)model).tournamentMatchDictionary[id];
//		((TournamentController)controller).GoToBattleScene();
//	}

//	public void OnClickCloseStageDetails() {
//		((TournamentController)controller).CloseStageDetails();
//	}

	public void OnClickStartTournamentMatchButton() {
		((TournamentController)controller).StartTournamentMatch();
	}

	public void InitializeScrollView() {
		GameObject tournamentElement;

		for (int i = 0; i < ((TournamentModel)model).tournamentMatchList.Count; i++) {

			tournamentElement = Instantiate(Resources.Load("Prefabs/TournamentElement", typeof(GameObject))) as GameObject;
			tournamentElement.GetComponent<TournamentElementController>().SetStageData(((TournamentModel)model).tournamentMatchList[((TournamentModel)model).tournamentMatchList.Count - (i + 1)]);
			tournamentElement.GetComponent<TournamentElementController>().CheckIfStageIsUnlocked();

			elementsList.Add(tournamentElement);
		}

		tournamentElement = Instantiate(Resources.Load("Prefabs/TournamentElement_Self", typeof(GameObject))) as GameObject;
		elementsList.Insert(((TournamentModel)model).tournamentMatchList.Count - GameData.instance.playerData.tournamentProgress, tournamentElement);
		
		for (int i = elementsList.Count - 1; i >= 0; i--) {
			elementsList[i].transform.SetParent(content.transform, false);
			elementsList[i].GetComponent<TournamentElementController>().SetRankValue(i + 1);
		}

		// Resize scrollable background based on number of elements
		elementWidth = tournamentElement.GetComponent<LayoutElement>().minWidth + content.spacing;
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(elementWidth * (((TournamentModel)model).tournamentMatchList.Count + 1) + content.padding.right + content.padding.bottom, rt.rect.height);

		GetComponentInChildren<ScrollRect>().horizontalScrollbar.value = 0;
		//StartCoroutine(ScrolldownCoroutine());
	}

	public IEnumerator ScrolldownCoroutine() {
		GetComponentInChildren<ScrollRect>().horizontalScrollbar.value = 1;
		yield return new WaitForSeconds(2.5f);

		while (GetComponentInChildren<ScrollRect>().horizontalScrollbar.value > 0) {
			GetComponentInChildren<ScrollRect>().horizontalScrollbar.value -= Time.fixedDeltaTime/2;
			yield return new WaitForEndOfFrame();
		}
	}

}
