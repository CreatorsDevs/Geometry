using UnityEngine;
using UnityEngine.UI;

public class BoostState : IPlayerState
{
    private PlayerStateMachine playerStateMachine;

    private Image m_BoostFillImage;
    private float boostDuration = 5f;
    private float boostTimer;
    public BoostState(PlayerStateMachine playerStateMachine)
    {
        this.playerStateMachine = playerStateMachine;
    }

    public void EnterState()
    {
        playerStateMachine.SetMoveSpeed(playerStateMachine.moveSpeed * 2f);
        boostTimer = boostDuration;
        ServiceLocator.Get<GameManager>().ActivateBoostSlider();
    }

    public void ExitState()
    {
        playerStateMachine.activateBoost = false;
        playerStateMachine.SetMoveSpeed(playerStateMachine.defaultMoveSpeed);
        ServiceLocator.Get<GameManager>().DeactivateBoostSlider();
    }

    public void UpdateState()
    {
        // Player will destroy hurdles on its path, this can be handled by collision logic elsewhere.
        // Time management for the boost duration
        boostTimer -= Time.deltaTime;
        if(m_BoostFillImage == null)
            m_BoostFillImage = ServiceLocator.Get<GameManager>().BoostFillImage;
        m_BoostFillImage.fillAmount = boostTimer / boostDuration;
        if(boostTimer <= 0)
        {
            playerStateMachine.SetState(playerStateMachine.NormalState);
        }
    }
}
