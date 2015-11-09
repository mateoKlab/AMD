using UnityEngine;
using System.Collections;
using Bingo;

public class Battle : BaseApplication<BattleModel, BattleView, BattleController>
{
    void Awake ()
	{
		GetComponent<BattleController> ().SpawnFighters ();

	}
}

