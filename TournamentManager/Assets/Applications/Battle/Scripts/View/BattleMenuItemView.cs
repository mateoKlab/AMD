using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class BattleMenuItemView : View <Battle, BattleMenuItemModel, BattleMenuItemController>
{
	public Text fighterName;
	public Text lvlLabel;
	//public Text hpLabel;
	public RawImage classIcon;
	public Image hpSlider;
	public Image hpSliderTrail;
	public Image deathIcon;
	public FighterSpriteController warriorPortrait;
	public FighterSpriteController magePortrait;

	void Start ()
	{

	}

	public void InitializeValues ()
	{
		model.fData = model.fighter.GetComponent<FighterModel>().fighterData;
		fighterName.text = model.fData.name;
		lvlLabel.text = "LVL  " + model.fData.level;
		classIcon.texture = classIcon.texture = Resources.Load("Sprites/ClassIcons/" + model.fData.fighterClass) as Texture;

		float hpFill = (float)model.fData.HP / (float)model.fData.maxHP;
		hpSlider.fillAmount = hpFill;

		SetPortrait(model.fData.fighterClass, model.fData.skinData);
	}

	public void UpdateHP() {
		float hpFill = (float)model.fData.HP / (float)model.fData.maxHP;

		hpSlider.fillAmount = hpFill;
		StopCoroutine("UpdateHPCoroutine");
		StartCoroutine("UpdateHPCoroutine");

		ShakePortrait();

	}

	IEnumerator UpdateHPCoroutine() {
		yield return new WaitForSeconds(0.2f);
		while (hpSliderTrail.fillAmount > hpSlider.fillAmount) {

			if (hpSlider.fillAmount > 0) {
				hpSliderTrail.fillAmount -= Time.fixedDeltaTime/2.5f;
			}
			else 
			{
				hpSliderTrail.fillAmount -= Time.fixedDeltaTime;
			}
			yield return new WaitForEndOfFrame();
		}
	}

	public void ShowDeathIcon (bool enabled)
	{
		deathIcon.gameObject.SetActive (enabled);
	}

	private void SetPortrait (Class fighterClass, FighterSkinData skinData)
	{
		warriorPortrait.gameObject.SetActive (false);
		magePortrait.gameObject.SetActive (false);
		
		switch (fighterClass) {
		case Class.Warrior:
		{
			warriorPortrait.SetFighterSkin (skinData);
			warriorPortrait.gameObject.SetActive (true);
			break;
		}
			
		case Class.Mage:
		{
			
			magePortrait.SetFighterSkin (skinData);
			magePortrait.gameObject.SetActive (true);
			break;
		}
			
		}
	}

	public void ShakePortrait() {
		if(warriorPortrait.gameObject.activeSelf) 
		{
			iTween.ShakePosition(warriorPortrait.gameObject, new Vector3 (.2f, 0, 0), 0.2f);
		}
		else if (magePortrait.gameObject.activeSelf) 
		{
			iTween.ShakePosition(magePortrait.gameObject, new Vector3 (.2f, 0, 0), 0.2f);
		}
	}

}
