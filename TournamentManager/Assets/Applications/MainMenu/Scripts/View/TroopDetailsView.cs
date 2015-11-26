using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Bingo;

public class TroopDetailsView : View
{
    private Text nameText;
    private Text atkText;
    private Text hpText;
    private Text costText;
    public FighterSpriteController troopSprite;

    public override void Awake()
    {
        base.Awake();
        nameText = transform.FindChild("Name").GetComponent<Text>();
        atkText = transform.FindChild("Attack").GetComponent<Text>();
        hpText = transform.FindChild("HP").GetComponent<Text>();
        costText = transform.FindChild("Cost").GetComponent<Text>();
        troopSprite = transform.FindChild("TroopIconBorder/TroopSprite").GetComponent<FighterSpriteController>();
    }

    public void SetName(string name)
    {
        nameText.text = name;
    }

    public void SetAtk(float atk)
    {
        atkText.text = Mathf.RoundToInt(atk).ToString("n0");
    }

    public void SetHP(float hp)
    {
        hpText.text = Mathf.RoundToInt(hp).ToString("n0");
    }

    public void SetCost(int cost)
    {
        costText.text = cost.ToString("n0");
    }

    public void SetIcon(FighterData fighterData)
    {
        // TODO building sprite everytime feels really slow, refactor later
        SpriteBuilder.instance.BuildSprite (troopSprite);
    }

}
