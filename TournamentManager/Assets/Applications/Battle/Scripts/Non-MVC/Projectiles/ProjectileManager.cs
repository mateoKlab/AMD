using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Temporary?
public enum ProjectileType {

	Fireball,
	Arrow
}

public class ProjectileManager : MonoBehaviour {

	public GameObject projectilePrefab;
	public GameObject cacheContainer;

	public int cacheSize;

	private List<GameObject> projectileCache = new List<GameObject> ();
	private bool firstInstance = false;

	private static ProjectileManager _instance;
	public static ProjectileManager instance {
		get { 
			if (!_instance) {
				GameObject newObject = new GameObject ();
				newObject.name = "GameData";
				newObject.AddComponent<ProjectileManager> ();

				_instance = newObject.GetComponent<ProjectileManager> ();
			}
			return _instance; 
		}
		private set { }
	}
	
	void Awake()
	{
		_instance = this;
		
		// Keep object alive between scenes.
		//DontDestroyOnLoad (gameObject);
		
		// If there is more than 1 of this object, destroy the second instance.
		if (!firstInstance && FindObjectsOfType (typeof(GameData)).Length > 1) {
			DestroyImmediate (gameObject);
		} else {
			firstInstance = true;
		}
	}

	// Use this for initialization
	void Start () {
		for (int i = 0; i < cacheSize ; i++) {
			GameObject newProjectile = GameObject.Instantiate (projectilePrefab);

			newProjectile.transform.parent = cacheContainer.transform;
			projectileCache.Add (newProjectile);

		}
	}
	
	public GameObject GetProjectile (Attack attackData, ProjectileType type)
	{
		if (projectileCache.Count > 0) {
			GameObject projectile = projectileCache [0];

			// Must call SetActive first to call Awake ().
			projectile.SetActive (true);

			projectile.GetComponent<ProjectileController> ().OnDestroyProjectile += OnDestroyProjectile;
			projectile.GetComponent<ProjectileModel> ().attackData = attackData;

			projectileCache.RemoveAt (0);

			return projectile;
		} else {
			GameObject newProjectile = GameObject.Instantiate (projectilePrefab);
			newProjectile.GetComponent<ProjectileModel> ().attackData = attackData;
			return newProjectile;
		}
	}

	void OnDestroyProjectile (GameObject projectile)
	{
		projectile.SetActive (false);
		projectileCache.Add (projectile);
	}
}
