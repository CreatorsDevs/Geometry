using System.Collections.Generic;
using UnityEngine;

public class HurdleManager : Singleton<HurdleManager>
{
    [SerializeField] private int maxHurdlesPerPlatform = 5;
    private float hurdleBufferZ = 5.0f;
    private float platformBufferX = 1.0f;
    private float platformBufferZ = 5.0f;
    private float platformWidth = 9.0f;
    private float platformLength = 100.0f;

    private Queue<GameObject> activeHurdles = new Queue<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    void Update()
    {
        Transform player = ServiceLocator.Get<PlatformManager>().GetPlayerTransform();
        if (player && activeHurdles.Count > 0 && player.position.z - hurdleBufferZ > activeHurdles.Peek().transform.position.z)
        {
            RemoveHurdle();
        }
    }

    public void SpawnHurdlesOnPlatform(Vector3 platformPosition)
    {
        int hurdlesToSpawn = Random.Range(1, maxHurdlesPerPlatform + 1);

        for (int i = 0; i < hurdlesToSpawn; i++)
        {
            GameObject hurdle = ServiceLocator.Get<ObjectPooler>().GetPooledObject("Hurdle");
            float randomX = Random.Range(platformPosition.x - platformWidth / 2 + platformBufferX, platformPosition.x + platformWidth / 2 - platformBufferX);
            float randomZ = platformPosition.z - platformLength / 2 + platformBufferZ + i * (platformLength - 2 * platformBufferZ) / hurdlesToSpawn;

            hurdle.transform.position = new Vector3(randomX, hurdle.transform.position.y, randomZ);
            activeHurdles.Enqueue(hurdle);
        }
    }

    void RemoveHurdle()
    {
        GameObject removedHurdle = activeHurdles.Dequeue();
        ServiceLocator.Get<ObjectPooler>().ReturnToPool("Hurdle", removedHurdle);
    }
}
