using UnityEngine;
using System.Collections;
using Bingo;
using UnityEngine.UI;

public class FacilityView : View
{
	public Text costLabel;
	public Slider progressSlider;
	public Button buildButton;
	public GameObject facility;
	public GameObject hammer;

	public override void Awake ()
	{
		base.Awake ();
		costLabel = GetComponentInChildren<Text>();
		progressSlider = GetComponentInChildren<Slider>();
		buildButton =  GetComponentInChildren<Button>();
		facility = transform.FindChild("FacilitySprite").gameObject;
		hammer = transform.FindChild("Icon").gameObject;
		progressSlider.gameObject.SetActive(false);
	}

	public void Start()
	{
		costLabel.text = ((FacilityModel)model).cost.ToString();
	}

	public void OnClickBuildButton() 
	{
		((FacilityController)controller).StartBuildingTownFacility();
	}
}
