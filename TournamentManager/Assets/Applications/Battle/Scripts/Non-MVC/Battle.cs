using UnityEngine;
using System.Collections;
using Bingo;

public class Battle : BaseApplication<BattleModel, BattleView, BattleController>
{
    void Start ()
	{
		// TEMP..
		GetComponent<BattleController> ().SpawnFighters ();
		SoundManager.instance.PlayBGM("Audio/BGM/01 RPG Battle Music");
		SoundManager.instance.PlaySFX("Audio/SFX/Horn");

	}


	public void Send (EventTags eventTag, params object[] args) 
	{
		Messenger.Send (eventTag, args);
	}

	public void AddListener (EventTags eventTag, Messenger.MessageDelegate callback) 
	{
		Messenger.AddListener (eventTag, callback);
	}
}

