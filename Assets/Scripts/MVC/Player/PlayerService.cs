using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerService : Singleton<PlayerService>
{
    [SerializeField] private PlayerScriptableObjectList playerScriptableObjectList;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }
    private void Start()
    {
        SpawnSelectedPlayer();
    }

    private PlayerController SpawnSelectedPlayer()
    {
        int randomNumber = (int)Random.Range(0, playerScriptableObjectList.playerObjects.Length);
        PlayerScriptableObject playerObject = playerScriptableObjectList.playerObjects[randomNumber];
        Debug.Log("Spawned player of type:" + playerObject.name);
        PlayerModel model = new(playerObject);
        PlayerController player = new(model, playerObject.playerView);
        return player;
    }
}
