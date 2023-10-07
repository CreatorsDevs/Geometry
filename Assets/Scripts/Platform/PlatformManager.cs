using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : Singleton<PlatformManager>
{
    [SerializeField] private int numberOfPlatforms = 4;
    [SerializeField] private float platformLength = 100.0f;
    private Queue<GameObject> activePlatforms = new Queue<GameObject>();
    private float spawnZ = 0.0f;
    private Transform player;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    void Start()
    {
        for (int i = 0; i < numberOfPlatforms; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (platformLength * 4))
        {
            SpawnPlatform();
        }

        if (player.position.z - platformLength > activePlatforms.Peek().transform.position.z)
        {
            RemovePlatform();
        }
    }

    void SpawnPlatform()
    {
        GameObject platform = ServiceLocator.Get<ObjectPooler>().GetPooledObject("Platform");
        platform.transform.position = Vector3.forward * spawnZ;
        platform.transform.rotation = Quaternion.identity;
        activePlatforms.Enqueue(platform);
        spawnZ += platformLength;

        ServiceLocator.Get<HurdleManager>().SpawnHurdlesOnPlatform(platform.transform.position);
    }

    void RemovePlatform()
    {
        GameObject removedPlatform = activePlatforms.Dequeue();
        ServiceLocator.Get<ObjectPooler>().ReturnToPool("Platform", removedPlatform);
    }

    public Transform GetPlayerTransform()
    {
        return player;
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        player = playerTransform;
    }
}
