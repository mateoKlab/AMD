using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class EditTeamView : View
{
	private Text teamCost;

	public override void Awake ()
	{
		base.Awake ();

		teamCost = transform.Find("PartyCost").GetComponent<Text>();
	}

	public void SetCost(int cost, int capacity)
	{
		teamCost.text = cost + " / " + capacity;
	}
}
