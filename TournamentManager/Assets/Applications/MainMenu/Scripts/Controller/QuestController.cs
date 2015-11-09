using UnityEngine;
using System.Collections;
using Bingo;

public class QuestController : Controller
{
	public void GoToQuest(params object[] args) {
		Debug.Log ("Quest #" + args[0]);
	}
}
