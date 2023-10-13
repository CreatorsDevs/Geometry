using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class PlayerService : Singleton<PlayerService>
{
    [SerializeField] private PlayerScriptableObjectList playerScriptableObjectList;
    private PlayerController playerController { get; }

    GameManager gameManager;

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
        gameManager = ServiceLocator.Get<GameManager>();
    }
    private void Start()
    {
        //if (gameManager.GameStarted)
            //SpawnSelectedPlayer();
    }

    internal PlayerController SpawnSelectedPlayer()
    {
        int randomNumber = (int)Random.Range(0, playerScriptableObjectList.playerObjects.Length);
        PlayerScriptableObject playerObject = playerScriptableObjectList.playerObjects[randomNumber];
        Debug.Log("Spawned player of type:" + playerObject.name);
        PlayerModel model = new(playerObject);
        PlayerController player = new(model, playerObject.playerView);

        PlatformManager platformManager = ServiceLocator.Get<PlatformManager>();
        PlayerStateMachine playerStateMachine = ServiceLocator.Get<PlayerStateMachine>();
        CameraFollow camera = ServiceLocator.Get<CameraFollow>();

        platformManager.SetPlayerTransform(player.PlayerView.transform);
        camera.SetPlayer(player.PlayerView.transform);
        playerStateMachine.SetDefaultMoveSpeed(model.MoveSpeed);

        return player;
    }
}
