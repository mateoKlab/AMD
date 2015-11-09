using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopController : Controller<MainMenu>
{
	private bool isAnActiveTroop;

	private Vector3 startPosition;
	private Transform startParent;
	private CanvasGroup canvasGroup;
	private GridLayoutGroup gridLayoutGroup;
	private int siblingIndex;
	
	public override void Awake() {
		base.Awake();

		canvasGroup = GetComponent<CanvasGroup>();
		gridLayoutGroup = transform.parent.GetComponent<GridLayoutGroup>();
	}

	public void OnBeginDrag()
	{
		startPosition = transform.position;
		startParent = transform.parent;
		canvasGroup.blocksRaycasts = false;
		gridLayoutGroup.enabled = false;

		siblingIndex = transform.GetSiblingIndex();
		transform.SetParent(app.controller.editTeamController.transform, true);
		transform.SetAsLastSibling();
	}

	public void OnEndDrag() 
	{
		canvasGroup.blocksRaycasts = true;
		
		// Snap to position after dragging if it is not dragged to an empty slot.
		//if(transform.parent == startParent)
		//{

		if(transform.parent == app.view.editTeamView.transform)
		{
			Debug.LogError("app.view.editTeamView.transform");
			transform.SetParent(startParent);	
		}
//		else if(transform.parent == startParent)
//		{
//			Debug.LogError("startParent");
//			transform.position = startPosition;
//		}

		transform.SetSiblingIndex(siblingIndex);

		gridLayoutGroup.enabled = true;
		Debug.LogError("TroopController OnEndDrag");
	}
}
