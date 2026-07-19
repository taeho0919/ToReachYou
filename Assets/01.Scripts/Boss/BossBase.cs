using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class BossBase : MonoBehaviour
{
    public BossData _bossData;
    private IState _currentBoss;

    
    private void Awake()
    {
        switch (_bossData.bossType)
        {
            case BossType.Bossj:
                _currentBoss = new BossJ(this, _bossData);
                break;
            case BossType.Bossf:
                _currentBoss=new BossF(this, _bossData);
                break;
            case BossType.Bossi:
                _currentBoss = new BossI(this, _bossData);
                break;

        }

        _currentBoss.Started();
    }

    private void Update()
    {
        _currentBoss?.Looped();

    }
}
