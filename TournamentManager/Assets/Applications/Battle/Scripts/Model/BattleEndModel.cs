using UnityEngine;
using System.Collections;
using Bingo;

public class BattleEndModel : Model
{
	public int exp;
	public int gold;

	public override void Awake() {
		base.Awake ();

		gold = GameData.instance.currentStage.goldReward;
		exp = GameData.instance.currentStage.xpReward;
	
	}
}
