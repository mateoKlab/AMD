using UnityEngine;
using System.Collections;
using Bingo;

public class ActiveTeamSlotController : Controller
{
	public int slotIndex;

	private EditTeamController _editTeamController;
	protected EditTeamController editTeamController
	{
		get
		{
			if(_editTeamController == null)
				_editTeamController = GameObject.Find("EditTeam").GetComponent<EditTeamController>();

			return _editTeamController;
		}
	}

	public override void Awake ()
	{
		base.Awake ();

		slotIndex = transform.GetSiblingIndex();
	}

	private bool isSlotOccupied()
	{
		// If a slot has no child, it means it's empty.
		return transform.childCount > 0;
	}

}
