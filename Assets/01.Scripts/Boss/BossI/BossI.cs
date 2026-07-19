using UnityEngine;
using System.Collections;

public class BossI : IState
{
    private BossBase _bossBase;
    private BossData _bossData;

    private int _patternIndex = 0; // 현재 몇 번째 순서인지

    public BossI(BossBase controller, BossData bd) => (_bossBase, _bossData) = (controller, bd);

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
        MirrorShield();
        yield return new WaitForSeconds(4f);
        NextPattern();
    }
    private IEnumerator Pattern2()
    {
       GoldShower();
        yield return new WaitForSeconds(5f);
        NextPattern();

    }
    private IEnumerator Pattern3()
    {
        Emoji();
        yield return new WaitForSeconds(2f);
        NextPattern();

    }
    private IEnumerator Pattern4()
    {
        
        yield return new WaitForSeconds(4f);
        NextPattern();

    }

    private IEnumerator Pattern5()
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
            case 5:
                _bossBase.StartCoroutine(Pattern5());
                break;
        }
    }

    private void MirrorShield()
    {
        var mirror = _bossBase.GetComponentsInChildren<BossIShield>(true);
        foreach (var mirrorShield in mirror)
        {
            if (!mirrorShield.gameObject.activeSelf)
                mirrorShield.gameObject.SetActive(true);
        }

    }

   private void GoldShower()
    {
        var gold = _bossBase.GetComponentsInChildren<BossGoldSpawn>(true);
        foreach (var goldShower in gold)
        {
            if (!goldShower.gameObject.activeSelf)
                goldShower.gameObject.SetActive(true);
        }
    }

    private void Emoji()
    {
        var emoji = _bossBase.GetComponentsInChildren<EmojiDrop>(true);
        foreach (var emojiDrop in emoji)
        {
            if (!emojiDrop.gameObject.activeSelf)
                emojiDrop.gameObject.SetActive(true);
        }
    }
}


