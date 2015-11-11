using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class TournamentView : View
{
	public VerticalLayoutGroup content;
	private float elementHeight;
	private Text rankText;

	public void Start()
	{
		//base.Start();
		InitializeScrollView();
	}

	public void OnClickCloseButton() {
		((TournamentController)controller).CloseTournamentPopUp();
	}

	public void OnClickTournamentButton(string id) {
		GameData.Instance.currentStage = ((TournamentModel)model).tournamentMatchDictionary[id];
		((TournamentController)controller).GoToBattleScene();
	}

	private void InitializeScrollView() {
		content = transform.FindChild("TournamentScrollView").GetComponentInChildren<VerticalLayoutGroup>();
		
		for (int i = 0; i < ((TournamentModel)model).tournamentMatchList.Count; i++) {
			GameObject tournamentElement = Instantiate(Resources.Load("Prefabs/TournamentElement", typeof(GameObject))) as GameObject;
			tournamentElement.transform.SetParent(content.transform, false);

			tournamentElement.GetComponent<TournamentElementController>().SetRankValue(i + 1);
			tournamentElement.GetComponent<TournamentElementController>().SetStageData(((TournamentModel)model).tournamentMatchList[((TournamentModel)model).tournamentMatchList.Count - (i + 1)]);
			tournamentElement.GetComponent<TournamentElementController>().CheckIfStageIsUnlocked();

			elementHeight = tournamentElement.GetComponent<LayoutElement>().minHeight + content.spacing;
		}
		
		RectTransform rt = content.GetComponent<RectTransform>();
		rt.sizeDelta = new Vector2(rt.rect.width, elementHeight * (((TournamentModel)model).tournamentMatchList.Count) + content.padding.top + content.padding.bottom);
		
		GetComponentInChildren<ScrollRect>().verticalScrollbar.value = 0;
	}

}
