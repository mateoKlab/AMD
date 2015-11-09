using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour {
	
	public PlayerData PlayerData = new PlayerData ();

	private bool firstInstance = false;
	
	// Singleton instance. _instance will be set on Awake().
	private static GameData _instance;
	public static GameData Instance {
		get { 
			if (!_instance) {
				GameObject newObject = new GameObject ();
				newObject.name = "GameData";
				newObject.AddComponent<GameData> ();

				_instance = newObject.GetComponent<GameData> ();
			}
			return _instance; 
		}
		private set { }
	}
	
	void Awake()
	{
		_instance = this;
		
		// Keep object alive between scenes.
		DontDestroyOnLoad(gameObject);
		
		// If there is more than 1 of this object, destroy the second instance.
		if (!firstInstance && FindObjectsOfType (typeof(GameData)).Length > 1) {
			DestroyImmediate (gameObject);
		} else {
			firstInstance = true;
		}
		
		// TODO: LOAD DATA...
	}

	public void Load ()
	{

	}

	public void Save ()
	{

	}

}
