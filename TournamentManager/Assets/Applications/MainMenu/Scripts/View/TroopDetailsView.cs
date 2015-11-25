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
    public SkeletonRenderer skeletonRenderer;

    public override void Awake()
    {
        base.Awake();
        nameText = transform.FindChild("Name").GetComponent<Text>();
        atkText = transform.FindChild("Attack").GetComponent<Text>();
        hpText = transform.FindChild("HP").GetComponent<Text>();
        costText = transform.FindChild("Cost").GetComponent<Text>();
        skeletonRenderer = transform.FindChild("TroopIconBorder/TroopSpine").GetComponent<SkeletonRenderer>();
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
        skeletonRenderer.GetComponent<HeroAttachmentLoader>().setType = (HeroAttachmentLoader.SetType) fighterData.fighterElement;
        skeletonRenderer.GetComponent<HeroAttachmentLoader>().ChangeSet();
    }

}
