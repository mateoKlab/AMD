using UnityEngine;
using System.Collections;
using Bingo;

public class BattleView : View<Battle>
{
    

	public void OnClickBackButton ()
	{
		((BattleController)controller).OnBackButtonClicked ();
	}
}

