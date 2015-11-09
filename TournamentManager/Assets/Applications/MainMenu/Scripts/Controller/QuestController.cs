using UnityEngine;
using System.Collections;
using Bingo;

public class QuestController : Controller
{
	public override void Awake() 
	{
		base.Awake();
		PopulateQuestList();
	}
	
	private void PopulateQuestList() 
	{
		// HACK Test code
		Debug.Log ("PopulateQuestList");
		
		foreach (Transform qPoint in GetComponentsInChildren<Transform>())
		{
			((QuestModel)model).questPoints.Add(qPoint);
		}
		
		StageData testData = new StageData();
		
		FighterData testFighter1 = new FighterData ();
		testFighter1.HP = 2000;
		testFighter1.ATK = 50;
		
		testData.enemies.Add (testFighter1);
		((QuestModel)model).questList.Add(testData);
		
		testData.enemies.Add (testFighter1);
		((QuestModel)model).questList.Add(testData);
		
	}

	public void GoToQuest(params object[] args) {
		Debug.Log ("Quest #" + args[0]);
	}
}
