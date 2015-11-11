using UnityEngine;
using System.Collections;
using Bingo;
using System.Collections.Generic;

public class TournamentModel : Model
{
	public Dictionary<string, StageData> tournamentMatchDictionary;
	public List<StageData> tournamentMatchList = new List<StageData>();
	public override void Awake() 
	{
		base.Awake();
		PopulateMatchesList();
	}
	
	private void PopulateMatchesList()
	{
		
		Dictionary<StageType, Dictionary<string, StageData>> stageDatabaseClone = GameData.instance.stageDatabase;
		tournamentMatchDictionary = stageDatabaseClone[StageType.Tournament];

		foreach (StageData sData in tournamentMatchDictionary.Values) {
			tournamentMatchList.Add(sData);
		}

		for (int i = 0; i <= GameData.instance.playerData.rank; i++) {
			if (!GameData.instance.playerData.unlockedStages.Contains(tournamentMatchList[i].id))
			{
				GameData.instance.playerData.unlockedStages.Add(tournamentMatchList[i].id);
			}
		}


	}
}
