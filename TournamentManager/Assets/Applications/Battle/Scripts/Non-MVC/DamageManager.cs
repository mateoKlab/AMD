using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Bingo;

public class DamageManager : BaseApplication <DamageUIModel, DamageUIView, DamageUIController> {

	public GameObject damagePrefab;
	public List<GameObject> damagePool;
	public int poolSize; 

	private static DamageManager _instance = null; //PROBLEM ON COPYING THE CAMERA OF FIX CAMERA FOLLOW. REMEMBER MEN

	public static DamageManager instance {
		get {
			return _instance;
		}
	}

	public override void Awake() {
		base.Awake();
		if(_instance == null)
		{
			_instance = this;
		}
	}

	void Start () {
		for (int i = 0; i < poolSize; i++) 
		{
			GameObject go = GameObject.Instantiate(damagePrefab);
			damagePool.Add(go);
			go.transform.SetParent(transform, false);
		}
	}
	
	public void ActivateDamageElement(Vector3 hitPos, int damage) {
		damagePool[0].SetActive(true);
		damagePool[0].transform.position = hitPos;
		damagePool[0].GetComponent<DamageUIController>().ShowDamage(damage);
		damagePool.RemoveAt(0);
	}

	public void AddToPool(GameObject element) {
		if (!damagePool.Contains(element)){
			element.SetActive(false);
			damagePool.Add(element);

		}
	}
}
