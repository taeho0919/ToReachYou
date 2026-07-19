using UnityEngine;

public enum BossType
{
    Bossj,
    Bossf,
    Bossi
}

[CreateAssetMenu(fileName ="BossData",menuName ="bossD")]
public class BossData : ScriptableObject
{
    public BossType bossType;
    public int[] patternOrder;
   
}

