using System.Collections;
using System.Linq;
using UnityEngine;

public class BossJ : IState
{
    private BossBase _bossBase;
    private BossData _bossData;

    private int _patternIndex = 0; // 현재 몇 번째 순서인지

    public BossJ(BossBase controller, BossData bd) => (_bossBase, _bossData) = (controller, bd);

    public void Started()
    {
        RunPattern(_bossData.patternOrder[_patternIndex]);
    }
    public void Looped()
    {

    }


    public void Stopped()
    {

    }
    private IEnumerator Pattern1()
    {
        RosePrison();
        yield return new WaitForSeconds(3f);
        NextPattern();
    }
    private IEnumerator Pattern2()
    {  
        RoseWhipV();
        yield return new WaitForSeconds(3f);
        NextPattern();

    }
    private IEnumerator Pattern3()
    {
        RoseWhip();
        yield return new WaitForSeconds(4f);
        NextPattern();

    }
    private IEnumerator Pattern4()
    {
        RoseArrow();
        yield return new WaitForSeconds(4f);
        NextPattern();

    }
    private IEnumerator Pattern5()
    {
        All();
        yield return new WaitUntil(() => AllInactive());
        NextPattern();

    }


    public void NextPattern()
    {
        if (_bossBase == null) return;

        _patternIndex = (_patternIndex + 1) % _bossData.patternOrder.Length;
        RunPattern(_bossData.patternOrder[_patternIndex]);
    }

    private void RunPattern(int patternNumber)
    {
        switch (patternNumber)
        {
            case 1:
                _bossBase.StartCoroutine(Pattern1());
                break;
            case 2:
                _bossBase.StartCoroutine(Pattern2());
                break;
            case 3:
                
                _bossBase.StartCoroutine(Pattern3());
                break;
             case 4:
                _bossBase.StartCoroutine(Pattern4());
                break;

            case 5:
                _bossBase.StartCoroutine(Pattern5());
                break;
        }
    }

    private void RosePrison()
    {
        var roses = _bossBase.GetComponentsInChildren<RosePrison>(true);
        foreach (var rose in roses)
        {
            if (!rose.gameObject.activeSelf)
                rose.gameObject.SetActive(true);
        }
    }

    private void RoseWhip()
    {
        var roses = _bossBase.GetComponentsInChildren<RoseWhip>(true);
        foreach (var rose in roses)
        {
            if (!rose.gameObject.activeSelf)
                rose.gameObject.SetActive(true);
        }
    }

    private void RoseWhipV()
    {
        var roses = _bossBase.GetComponentsInChildren<BossWhipV>(true);
        foreach (var rose in roses)
        {
            if (!rose.gameObject.activeSelf)
                rose.gameObject.SetActive(true);
        }
    }

    private void RoseArrow()
    {
        var needles = Object.FindObjectsByType<SpawnHNeedle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        foreach (var needle in needles)
        {
            if (!needle.gameObject.activeSelf)
                needle.gameObject.SetActive(true);
        }
    }
    private void All()
    {
        RoseWhip();
        RosePrison();
        RoseWhipV();
        RoseArrow();
    }
    private bool AllInactive()
    {
        bool rosePrisonDone = _bossBase.GetComponentsInChildren<RosePrison>(true)
            .All(r => !r.gameObject.activeSelf);

        bool roseWhipDone = _bossBase.GetComponentsInChildren<RoseWhip>(true)
            .All(r => !r.gameObject.activeSelf);

        bool roseWhipVDone = _bossBase.GetComponentsInChildren<BossWhipV>(true)
            .All(r => !r.gameObject.activeSelf);

        bool roseArrowDone = Object.FindObjectsByType<SpawnHNeedle>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .All(n => !n.gameObject.activeSelf);

        return rosePrisonDone && roseWhipDone && roseWhipVDone && roseArrowDone;
    }
}
