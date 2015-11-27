using UnityEngine;
using System.Collections;
using Bingo;

public class BattleModel : Model<Battle>
{
    // MVCCodeEditor GENERATED CODE - DO NOT MODIFY //
    
    [Inject]
    public BattleEndModel battleEndModel { get; private set; }
    
    [Inject]
    public BattleMenuModel battleMenuModel { get; private set; }
    
    //////// END MVCCodeEditor GENERATED CODE ////////
    
	public PlayerData PlayerData;
	public StageData currentStage;


	// Temporary.
	void Start ()
	{
		currentStage = GameData.instance.currentStage;
	}
}

