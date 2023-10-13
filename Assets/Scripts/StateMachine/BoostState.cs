using UnityEngine;

public class BoostState : IPlayerState
{
    private PlayerStateMachine playerStateMachine;

    private float boostDuration = 5f;
    private float boostTimer;
    public BoostState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public void EnterState()
    {
        //throw new System.NotImplementedException();
        playerStateMachine.SetMoveSpeed(playerStateMachine.moveSpeed * 2f);
        //playerStateMachine.moveSpeed *= 2f;
        boostTimer = boostDuration;
    }

    public void ExitState()
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Exiting boost state!");
        playerStateMachine.activateBoost = false;
        playerStateMachine.SetMoveSpeed(playerStateMachine.defaultMoveSpeed);
        //playerStateMachine.moveSpeed = playerStateMachine.defaultMoveSpeed;
    }

    public void UpdateState()
    {
        //throw new System.NotImplementedException();
        // Player will destroy hurdles on its path, this can be handled by collision logic elsewhere.
        // Time management for the boost duration
        boostTimer -= Time.deltaTime;
        if(boostTimer <= 0)
        {
            playerStateMachine.SetState(playerStateMachine.NormalState);
        }
    }
}
