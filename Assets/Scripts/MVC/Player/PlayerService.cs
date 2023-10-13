using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[DefaultExecutionOrder(11)]
public class PlayerService : Singleton<PlayerService>
{
    [SerializeField] private PlayerScriptableObjectList playerScriptableObjectList;
    private PlayerController playerController { get; }

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
        PlatformManager platformManager = ServiceLocator.Get<PlatformManager>();
        CameraFollow camera =ServiceLocator.Get<CameraFollow>();
        platformManager.SetPlayerTransform(player.PlayerView.transform);
        camera.SetPlayer(player.PlayerView.transform);
        return player;
    }
}
