using UnityEngine;
using System.Collections;
using Bingo;

public class BattleModel : Model<Battle>
{
	public PlayerData PlayerData;
	public StageData currentStage;


	// Temporary.
	void Start ()
	{
		currentStage = GameData.instance.currentStage;
	}
}

