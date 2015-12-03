using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TrainingGroundsView : View<MainMenu, TrainingGroundsModel, TrainingGroundsController>
{
    public Text costLabel;
    public Slider progressSlider;
    public Button buildButton;
    public Image facilitySprite;
    public GameObject hammer;
    
    public override void Awake ()
    {
        base.Awake ();
        
        costLabel = GetComponentInChildren<Text>();
        progressSlider = GetComponentInChildren<Slider>();
        facilitySprite = transform.FindChild("FacilitySprite").GetComponent<Image>();
        hammer = transform.FindChild("Icon").gameObject;
        progressSlider.gameObject.SetActive(false);
        
        buildButton =  GetComponentInChildren<Button>();
        buildButton.onClick.AddListener(() => OnClickBuildButton());
    }
    
    public void Start()
    {
        costLabel.text = model.cost.ToString();
    }
    
    public void OnClickBuildButton() 
    {
        controller.StartBuildingTownFacility();
    }
    
    public void UpdateCost()
    {
        costLabel.text = model.cost.ToString();
    }
    
    public void UpdateFacilitySprite(Sprite sprite)
    {
        facilitySprite.sprite = sprite;
    }
}
