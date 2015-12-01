using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class TournamentModel : Model
{
	public Dictionary<string, StageData> tournamentMatchDictionary;
	public List<StageData> tournamentMatchList = new List<StageData>();
	//public int matchCount;

//	public override void Awake() 
//	{
//		base.Awake();
//		PopulateMatchesList();
//	}
	
	public void Awake()
	{
		StageDatabase stageDatabaseClone = GameDatabase.stageDatabase;//GameData.instance.gameDatabase.stageDatabase;

		tournamentMatchDictionary = stageDatabaseClone[StageType.Tournament];

		foreach (StageData sData in tournamentMatchDictionary.Values) 
		{
			tournamentMatchList.Add(sData);
		}
		
		GameData.instance.playerData.tournamentMatchCount = tournamentMatchList.Count;

	}
}
