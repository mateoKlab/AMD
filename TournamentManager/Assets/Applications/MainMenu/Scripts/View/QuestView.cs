using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class QuestView : View
{
	public void OnClickQuestPoint(string stageName) {
		((QuestController)controller).GoToQuest(((QuestModel)model).questDictionary[stageName]);
	}
}
