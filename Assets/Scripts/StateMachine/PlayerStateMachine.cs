using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(30)]
public class PlayerStateMachine : Singleton<PlayerStateMachine>
{
    public IPlayerState CurrentState { get; private set; }
    public IPlayerState NormalState { get; private set; }
    public IPlayerState BoostState { get; private set; }
   
    private PlayerService playerService;
    internal float moveSpeed;
    internal float defaultMoveSpeed;
    internal bool activateBoost = false;

    private bool m_StateInitialized = false;

    public void SetDefaultMoveSpeed(float defaultMoveSpeed)
    {
       this.defaultMoveSpeed = defaultMoveSpeed;
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);

        NormalState = new NormalState(this);
        BoostState = new BoostState(this);
    }

    private void Start()
    {
        //Debug.Log("Start function to set the normal state called within SM!");
    }

    private void Update()
    {
        if (defaultMoveSpeed > 0 && !m_StateInitialized)
        { 
            SetState(NormalState);
            m_StateInitialized = true;
        }

        CurrentState?.UpdateState();
        //GameManager.Instance.SetPlayerModelMoveSpeed(moveSpeed);
        ServiceLocator.Get<GameManager>().SetPlayerModelMoveSpeed(moveSpeed);
        //Debug.Log("Updating the state within SM!");
        /*Debug.Log("Default Move Speed is:" + defaultMoveSpeed);
        Debug.Log("Move Speed is:" + moveSpeed);*/
    }

    public void SetState(IPlayerState newState)
    {
        CurrentState?.ExitState();
        //Debug.Log("Boost activated within SM!");
        CurrentState = newState;
        CurrentState?.EnterState();
    }

    public void ActivateBoost()
    {
        
        activateBoost = true;
    }
}
