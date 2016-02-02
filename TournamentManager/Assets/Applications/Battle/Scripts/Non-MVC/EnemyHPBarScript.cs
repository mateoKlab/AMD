using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHPBarScript : MonoBehaviour {

	private FighterData fData;
	public Image hpSliderTrail;
	public Image hpSlider;
	public GameObject fighterObject;

	void Update() {
		if (fighterObject != null) 
		{
			transform.position = fighterObject.transform.position + Vector3.up * 2;
		}
	}

	public void AttachToFighter(GameObject fighter) {
		fighterObject = fighter;
		fData = fighter.GetComponent<FighterModel>().fighterData;
	}

	public void UpdateHP(GameObject fo ,FighterData fd) {
		fighterObject = fo;
		fData = fd;
		gameObject.SetActive(true);
	
		float hpFill = (float)fData.HP / (float)fData.maxHP;

		hpSlider.fillAmount = hpFill;
			

		StopCoroutine("UpdateHPCoroutine");
		StartCoroutine("UpdateHPCoroutine");
		
	}
	
	IEnumerator UpdateHPCoroutine() {
		yield return new WaitForSeconds(0.2f);
		while (hpSliderTrail.fillAmount > hpSlider.fillAmount) {
			
			if (hpSlider.fillAmount > 0) {
				hpSliderTrail.fillAmount -= Time.fixedDeltaTime/1.5f;
			}
			else 
			{
				hpSliderTrail.fillAmount -= Time.fixedDeltaTime * 2;
			}
			yield return new WaitForEndOfFrame();
		}

		if (hpSlider.fillAmount <= 0) {
			gameObject.SetActive(false);
			yield break;
		}
		yield return new WaitForSeconds(2);
		gameObject.SetActive(false);
	}
}
