using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class SimplePoll
{
    private static Dictionary<PoolType, Poll> pollInstance = new Dictionary<PoolType, Poll>();
    //khoi tao pool moi
    public static void PreLoad(GameUnit prefab, int amout, Transform parent)
    {
        if (prefab == null)
        {
            Debug.LogError("Null");
            return;
        }
        if (!pollInstance.ContainsKey(prefab.PoolType) || pollInstance[prefab.PoolType] == null)
        {
            Poll p = new Poll();
            p.PreLoad(prefab, amout, parent);
            pollInstance[prefab.PoolType] = p;
        }
    }
    //lay phan tu ra
    public static T Spawn<T>(PoolType poolType, Vector3 pos, Quaternion rota) where T : GameUnit
    {
        if(!pollInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType+"is not preload");
            return null;
        }
        return pollInstance[poolType].Spawn(pos, rota) as T;
    }
    //tra phan tu vao
    public static void Desspawn(GameUnit unit)
    {
        if (!pollInstance.ContainsKey(unit.PoolType))
        {
            Debug.LogError(unit.PoolType+"is not preload");          
        }
         pollInstance[unit.PoolType].DesSpawn(unit);
    }
    //thu thap phan tu
    public static void Collect(PoolType poolType)
    {
        if (!pollInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "is not preload");
        }
        pollInstance[poolType].Collect();
    }
    //thu thap tat ca phan tu
    public static void CollectAll()
    {
        foreach(var item in pollInstance.Values)
        {
            item.Collect();
        }
    }
    //destroy 1 pool
    public static void Release(PoolType poolType)
    {
        if (!pollInstance.ContainsKey(poolType))
        {
            Debug.LogError(poolType + "is not preload");
        }
        pollInstance[poolType].Release();
    }
    //destroy tat ca pool
    public static void RealeaseAll()
    {
        foreach (var item in pollInstance.Values)
        {
            item.Release();
        }
    }
}
public class Poll
{
    Transform parent;
    GameUnit prefab;
    //list chua cac unit dang o trong poll
    Queue<GameUnit> inactives = new Queue<GameUnit>();
    //list chua cac unit dang duoc su dung
    List<GameUnit> actives = new List<GameUnit>();
    //khoi tao pool
    public void PreLoad(GameUnit prefab, int amount, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;
        for (int i = 0; i < amount; i++)
        {
            DesSpawn(GameObject.Instantiate(prefab, parent));
        }
    }
    //lay phan tu pool
    public GameUnit Spawn(Vector3 pos, Quaternion rota)
    {
        GameUnit unit;
        if (inactives.Count <= 0)
        {
            unit = GameObject.Instantiate(prefab, parent);
        }
        else
        {
            unit = inactives.Dequeue();
        }
        unit.TF.SetLocalPositionAndRotation(pos, rota);
        actives.Add(unit);
        unit.gameObject.SetActive(true);
        return unit;
    }
    //tra phan tu vao trong pool
    public void DesSpawn(GameUnit unit)
    {
        if (unit != null && unit.gameObject.activeSelf)
        {
            actives.Remove(unit);
            inactives.Enqueue(unit);
            unit.gameObject.SetActive(false);
        }
    }
    //thu thập tất cả các phần tử dang su dung ve pool
    public void Collect()
    {
        while (actives.Count > 0)
        {
            DesSpawn(actives[0]);
        }
    }
    //destroy tat ca phan tu
    public void Release()
    {
        Collect();
        while (inactives.Count > 0)
        {
            GameObject.Destroy(inactives.Dequeue().gameObject);
        }
        inactives.Clear();
    }
}
