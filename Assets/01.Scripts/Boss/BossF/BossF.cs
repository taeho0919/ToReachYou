using System.Collections;
using System.Linq;
using UnityEngine;

public class BossF : IState
{

    private BossBase _bossBase;
    private BossData _bossData;

    private int _patternIndex = 0; // 현재 몇 번째 순서인지

    public BossF(BossBase controller, BossData bd) => (_bossBase, _bossData) = (controller, bd);

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
        CloudUp();
        yield return new WaitForSeconds(3f);
        NextPattern();
    }
    private IEnumerator Pattern2()
    {
        CloudDown();
        yield return new WaitForSeconds(3f);
        NextPattern();

    }
    private IEnumerator Pattern3()
    {
      
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

    private void CloudUp()
    {
        var clouds = _bossBase.GetComponentsInChildren<CloudUpDown>(true);

        foreach (var cloud in clouds)
        {
            if (cloud.type == CloudAttackType.Up)
                cloud.gameObject.SetActive(true);
            else
                cloud.gameObject.SetActive(false);
        }
    }

    private void CloudDown()
    {
        var clouds = _bossBase.GetComponentsInChildren<CloudUpDown>(true);

        foreach (var cloud in clouds)
        {
            if (cloud.type == CloudAttackType.Down)
                cloud.gameObject.SetActive(true);
            else
                cloud.gameObject.SetActive(false);
        }
    }

}
