using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class PlatformManager : Singleton<PlatformManager>
{
    [SerializeField] private int numberOfPlatforms = 4;
    [SerializeField] private float platformLength = 100.0f;   
    private Queue<GameObject> activePlatforms = new Queue<GameObject>();
    private float spawnZ = 0.0f;
    private Transform player;

    GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
        gameManager = ServiceLocator.Get<GameManager>();
    }

    public void Start()
    {
        if(gameManager.GameStarted)
        {
            activePlatforms.Clear();
            for (int i = 0; i < numberOfPlatforms; i++)
            {
                SpawnPlatform();
            }
        }   
    }

    void Update()
    {
        if (gameManager.GameStarted && player != null)
        {
            if (player.position.z > spawnZ - (platformLength * 4))
            {
                SpawnPlatform();
            }

            if (activePlatforms.Count > 0 && player.position.z - platformLength > activePlatforms.Peek().transform.position.z)
            {
                RemovePlatform();
            }
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
