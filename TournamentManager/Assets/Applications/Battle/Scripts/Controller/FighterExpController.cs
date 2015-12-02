using UnityEngine;
using System.Collections;
using Bingo;

public class FighterExpController : Controller
{
    private bool isActive;
    private float expGained;

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
        // HACK TEST VALUES
        expGained = Random.Range(30,101);
        ((FighterExpView)view).SetExpGained(expGained);
        ((FighterExpView)view).SetCurrentExpOnSlider(0);
    }

    public void AnimateExpSlider()
    {
        StopCoroutine("AnimateExpSliderCoroutine");
        StartCoroutine("AnimateExpSliderCoroutine");
    }

    public IEnumerator AnimateExpSliderCoroutine()
    {
        float count = 0;
        while(count <= expGained)
        {
            // HACK TEST VALUES
            float expRatio = count / 100f;
            ((FighterExpView)view).SetCurrentExpOnSlider(expRatio);
            count++;
            yield return new WaitForEndOfFrame();
        }

        yield return null;
    }
}
