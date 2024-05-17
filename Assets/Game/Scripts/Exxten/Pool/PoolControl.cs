using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolControl : MonoBehaviour
{
    public PooAmout[] poolAmounts;
    private void Awake()
    {
        for (int i = 0; i < poolAmounts.Length; i++)
            SimplePoll.PreLoad(poolAmounts[i].prefab, poolAmounts[i].amount, poolAmounts[i].parent);
    }
}
[System.Serializable]
public class PooAmout
{
    public GameUnit prefab;
    public Transform parent;
    public int amount;
}
public enum PoolType
{
    BulletHammer = 0,
    BulletLillpop = 1,
    BulletKnife = 2,
    BulletCandy = 3,
    BulletBoomerang = 4,
    BulletSwirly = 5,
}
