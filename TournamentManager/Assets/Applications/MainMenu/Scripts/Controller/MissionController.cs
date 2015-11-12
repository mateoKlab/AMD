using UnityEngine;
using System.Collections;
using Bingo;

public class MissionController : Controller
{
	public void GoToMission(params object[] args) {
		Debug.Log ("Mission: " + args[0]);
	}
}
