using UnityEngine;
using System.Collections;
using Bingo;

public class FighterExpController : Controller
{
    private bool isActive;

    void Start()
    {
        ((FighterExpView)view).EnableLevelupBanner(false);
        gameObject.SetActive(isActive);
    }

    public void EnableFighter(bool enabled)
    {
        isActive = enabled;
        gameObject.SetActive(isActive);
    }

    public void SetFighterDetails(FighterData fd)
    {
        ((FighterExpView)view).SetSprite(fd.skinData);
        ((FighterExpView)view).SetName(fd.name);
        ((FighterExpView)view).SetLevel(fd.level);
        ((FighterExpView)view).SetExpGained(100);
        ((FighterExpView)view).SetSliderValue(Random.value);
    }
}
