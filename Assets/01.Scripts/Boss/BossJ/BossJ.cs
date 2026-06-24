using UnityEngine;
using System.Collections;

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
        Debug.Log("Boss1 - RosePrison 시작");
        RosePrison();
        yield return new WaitForSeconds(3f);
        NextPattern();
    }
    private IEnumerator Pattern2()
    {  
        Debug.Log("Boss1 - Pattern2 시작");
        RoseWhipV();
        yield return new WaitForSeconds(3f);
        NextPattern();

    }
    private IEnumerator Pattern3()
    {
        
        Debug.Log("Boss1 - Pattern3 시작");
        RoseWhip();
        yield return new WaitForSeconds(4f);
        NextPattern();

    }
    private IEnumerator Pattern4()
    {

       
        yield return new WaitForSeconds(4f);
        NextPattern();

    }

    public void NextPattern()
    {
        if (_bossBase == null) return;

        _patternIndex = (_patternIndex + 1) % _bossData.patternOrder.Length;
        Debug.Log($"NextPattern 호출 - index: {_patternIndex}, pattern: {_bossData.patternOrder[_patternIndex]}");
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
        }
    }

    private void RosePrison()
    {
        var roses = _bossBase.GetComponentsInChildren<RosePrison>(true); // true = 비활성화된 것도 포함
        foreach (var rose in roses)
        {
            rose.gameObject.SetActive(true);
        }

    }

    private void RoseWhip()
    {
        var roses = _bossBase.GetComponentsInChildren<RoseWhip>(true); // true = 비활성화된 것도 포함
        foreach (var rose in roses)
        {
            rose.gameObject.SetActive(true);
        }
    }
    private void RoseWhipV()
    {
        var roses = _bossBase.GetComponentsInChildren<BossWhipV>(true); // true = 비활성화된 것도 포함
        foreach (var rose in roses)
        {
            rose.gameObject.SetActive(true);
        }
    }
}
