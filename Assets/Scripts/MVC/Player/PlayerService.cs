using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(20)]
public class PlayerService : Singleton<PlayerService>
{
    [SerializeField] private PlayerScriptableObjectList playerScriptableObjectList;
    private PlayerController playerController { get; }

    private PlatformManager m_PlatformManager;
    private PlayerStateMachine m_PlayerStateMachine;
    private CameraFollow m_Camera;
    //private CameraFollow camera { get; }

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

        if(m_PlatformManager == null)
            m_PlatformManager = ServiceLocator.Get<PlatformManager>();
        if(m_PlayerStateMachine == null)
            m_PlayerStateMachine = ServiceLocator.Get<PlayerStateMachine>();
        if(m_Camera == null)
            m_Camera = ServiceLocator.Get<CameraFollow>();

        m_PlatformManager.SetPlayerTransform(player.PlayerView.transform);
        m_Camera.SetPlayer(player.PlayerView.transform);
        m_PlayerStateMachine.SetDefaultMoveSpeed(model.MoveSpeed);

        return player;
    }
}
