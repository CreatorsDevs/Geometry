using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolItem
{
    public string tag;
    public GameObject prefab;
    public int size;
}

public class ObjectPooler : Singleton<ObjectPooler>
{
    public List<PoolItem> itemsToPool;
    private Dictionary<string, Queue<GameObject>> objectPools;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);

        objectPools = new Dictionary<string, Queue<GameObject>>();

        foreach (var item in itemsToPool)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < item.size; i++)
            {
                GameObject obj = Instantiate(item.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            objectPools.Add(item.tag, objectPool);
        }
    }

    public GameObject GetPooledObject(string tag)
    {
        if (!objectPools.ContainsKey(tag) || objectPools[tag].Count == 0)
        {
            foreach (var item in itemsToPool)
            {
                if (item.tag == tag)
                {
                    GameObject obj = Instantiate(item.prefab);
                    obj.SetActive(false);
                    objectPools[tag].Enqueue(obj);
                    return obj;
                }
            }
            Debug.LogError($"No object of tag {tag} available in pool. Ensure your ObjectPooler settings are correct.");
            return null;
        }

        GameObject pooledObject = objectPools[tag].Dequeue();
        pooledObject.SetActive(true);
        return pooledObject;
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!objectPools.ContainsKey(tag))
        {
            Debug.LogError($"Pool with tag {tag} doesn't exist. Ensure your ObjectPooler settings are correct.");
            return;
        }

        obj.SetActive(false);
        objectPools[tag].Enqueue(obj);
    }
}
