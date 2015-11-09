using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class QuestView : View
{
	public void OnClickQuestPoint(Transform t) {
		((QuestController)controller).GoToQuest(((QuestModel)model).questPoints.IndexOf(t));
	}
}
