using System.Collections.Generic;
using UnityEngine;

public class HurdleManager : Singleton<HurdleManager>
{
    [SerializeField] private int maxHurdlesPerPlatform = 15;
    [SerializeField] private int minHurdlesPerPlatform = 8;
    [SerializeField] private float startingHurdleSpawnPosition = 15.0f;
    private bool isInitialSpawn = true;
    private float hurdleBufferZ = 10.0f;
    private float platformBufferX = 1.0f;
    private float platformBufferZ = 5.0f;
    private float platformWidth = 9.0f;
    private float platformLength = 100.0f;

    [SerializeField]
    private Queue<GameObject> activeHurdles = new Queue<GameObject>();

    public ParticleSystem hurdleParticle;

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
        int hurdlesToSpawn = Random.Range(minHurdlesPerPlatform, maxHurdlesPerPlatform + 1);

        // Calculate starting z-position based on player's position and add a buffer of 15 units.
        Transform player = ServiceLocator.Get<PlatformManager>().GetPlayerTransform();
        float startingZPosition = platformPosition.z - platformLength / 2 + platformBufferZ;

        if (isInitialSpawn && player)
        {
            startingZPosition = player.position.z + startingHurdleSpawnPosition;
            isInitialSpawn = false;  // Reset the flag so subsequent spawns don't have the offset
        }

        for (int i = 0; i < hurdlesToSpawn; i++)
        {
            GameObject hurdle = ServiceLocator.Get<ObjectPooler>().GetPooledObject("Hurdle");
            float randomX = Random.Range(platformPosition.x - platformWidth / 2 + platformBufferX, platformPosition.x + platformWidth / 2 - platformBufferX);
            float randomZ = startingZPosition + i * (platformLength - 2 * platformBufferZ) / hurdlesToSpawn;

            hurdle.transform.position = new Vector3(randomX, hurdle.transform.position.y, randomZ);
            activeHurdles.Enqueue(hurdle);
        }
    }

    public void RemoveHurdle(GameObject hurdleToRemove = null)
    {
        if(hurdleToRemove == null)
        {
            GameObject removedHurdle = activeHurdles.Dequeue();
            Instantiate(hurdleParticle, removedHurdle.transform.position, Quaternion.identity);
            ServiceLocator.Get<ObjectPooler>().ReturnToPool("Hurdle", removedHurdle);
        }
        else
        {
            // Remove specific hurdle,
            // remove all the components before this hurdle and then remove this hurdle from queue

            // Instantiate particle effect at the location of the collided hurdle
            Instantiate(hurdleParticle, hurdleToRemove.transform.position, Quaternion.identity);

            // Use a temporary queue to store hurdles until we find the collided one
            Queue<GameObject> tempQueue = new Queue<GameObject>();

            while (activeHurdles.Count > 0)
            {
                GameObject currentHurdle = activeHurdles.Dequeue();

                // If we found the collided hurdle in the queue
                if (currentHurdle == hurdleToRemove)
                {
                    // Return it to the pool
                    ServiceLocator.Get<ObjectPooler>().ReturnToPool("Hurdle", currentHurdle);
                    break; // Exit the loop
                }
                else
                {
                    // If it's not the collided hurdle, store it temporarily
                    tempQueue.Enqueue(currentHurdle);
                }
            }

            // Return all the hurdles before the collided one to the pool and empty the temporary queue
            while (tempQueue.Count > 0)
            {
                GameObject hurdle = tempQueue.Dequeue();
                ServiceLocator.Get<ObjectPooler>().ReturnToPool("Hurdle", hurdle);
            }
        }
    }
}
