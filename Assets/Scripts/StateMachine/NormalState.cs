using UnityEngine;

public class NormalState : IPlayerState
{
    private readonly PlayerStateMachine playerStateMachine;
    public NormalState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }
    public void EnterState()
    {
        //throw new System.NotImplementedException();
        //Debug.Log("Default speed when entringh normal tate: " + playerStateMachine.defaultMoveSpeed);
        playerStateMachine.SetMoveSpeed(playerStateMachine.defaultMoveSpeed);
    }

    public void ExitState()
    {
        //throw new System.NotImplementedException();
    }

    public void UpdateState()
    {
        //throw new System.NotImplementedException();
        if (playerStateMachine.activateBoost)
        {
            playerStateMachine.SetState(playerStateMachine.BoostState);
        }
    }
}
