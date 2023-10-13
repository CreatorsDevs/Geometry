using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(30)]
public class PlayerStateMachine : Singleton<PlayerStateMachine>
{
    [SerializeField] private float timeToMakeBoostAvailable = 15f;

    public IPlayerState CurrentState { get; private set; }
    public IPlayerState NormalState { get; private set; }
    public IPlayerState BoostState { get; private set; }
    
    private PlayerService playerService;
    internal float moveSpeed;
    internal float defaultMoveSpeed;
    internal bool activateBoost = false;

    private bool m_CanCountdownForBoostAvailability = false;
    private float m_CurrTime = 0f;
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
        ServiceLocator.Get<ObserverSystem>().OnSwipe += HandleSwipe;
    }

    private void OnDestroy()
    {
        ServiceLocator.Get<ObserverSystem>().OnSwipe -= HandleSwipe;
    }

    private void HandleSwipe()
    {
        // To make sure we only start the boost available count down after we have swiped.
        m_CanCountdownForBoostAvailability = true;
    }

    private void Update()
    {
        if (defaultMoveSpeed > 0 && !m_StateInitialized)
        { 
            SetState(NormalState);
            m_StateInitialized = true;
        }

        CurrentState?.UpdateState();
        
        ServiceLocator.Get<GameManager>().SetPlayerModelMoveSpeed(moveSpeed);

        if(m_CanCountdownForBoostAvailability && !activateBoost)
        {
            m_CurrTime += Time.deltaTime;

            if(m_CurrTime >= timeToMakeBoostAvailable)
            {
                ServiceLocator.Get<ObserverSystem>().NotifyBoostAvailable();
                m_CurrTime = 0f;
            }
        }
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
        ServiceLocator.Get<ObserverSystem>().NotifyBoostActivated();
    }
}
