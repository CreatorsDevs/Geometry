using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController
{
    private readonly float m_AngleThresholdForY = 30; // In Degrees
    private Vector3 m_PositionToMoveTo;
    private bool m_JumpAllowed = false;
    private bool m_CanMoveRight = false;
    private bool m_CanMoveLeft = false;
    private Rigidbody rb;
    private Transform m_transform;

    public PlayerModel PlayerModel { get; }
    public PlayerView PlayerView { get; }

    public PlayerController(PlayerModel playerModel, PlayerView playerView)
    {
        PlayerModel = playerModel;
        PlayerView = GameObject.Instantiate<PlayerView>(playerView);
        PlayerView.SetPlayerController(this);
    }

    public void start()
    {
        
    }

    public void Initialize()
    {
        m_transform = PlayerView.transform;
        rb = PlayerView.GetRigidBody();
    }


    internal void Run()
    {
        MoveForward();
        MoveLeft();
        MoveRight();
        Jump();
    }

    public bool SwipeCheck(Vector2 touchStartPos, Vector2 touchEndPos)
    {
        Vector2 swipeDelta = touchEndPos - touchStartPos;
        Vector2 inputDirection = swipeDelta.normalized;
        Vector2 baseVector = touchEndPos.x <= touchStartPos.x ? Vector2.left : Vector2.right;
        float relation = Vector2.Dot(baseVector, inputDirection);
        bool isInputInY = relation <= Mathf.Cos(Mathf.Deg2Rad * m_AngleThresholdForY);

        if (isInputInY && (touchEndPos.y > touchStartPos.y) && rb.velocity.y == 0)
            { m_JumpAllowed = true; }
        if ((Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) && (swipeDelta.x > 0) && (rb.velocity.x == 0))
            { m_CanMoveRight = true; }
        if ((Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) && (swipeDelta.x < 0) && (rb.velocity.x == 0))
            { m_CanMoveLeft = true; }

        return m_JumpAllowed || m_CanMoveLeft || m_CanMoveRight;
    }

    private void MoveRight()
    {
        if(m_CanMoveRight)
        {
            m_PositionToMoveTo = m_transform.position + new Vector3(PlayerModel.MoveDistance, 0, 0);
            LerpPosition(m_PositionToMoveTo, PlayerModel.SmoothSwipeTime);
            m_CanMoveRight = false;
        }
    }

    private void MoveLeft()
    {
        if(m_CanMoveLeft)
        {
            m_PositionToMoveTo = m_transform.position + new Vector3(-PlayerModel.MoveDistance, 0, 0);
            LerpPosition(m_PositionToMoveTo, PlayerModel.SmoothSwipeTime);
            m_CanMoveLeft = false;
        }
    }

    private void Jump()
    {
        if(m_JumpAllowed)
        {
            m_PositionToMoveTo = m_transform.position + new Vector3(0, PlayerModel.JumpHeight, 0);
            LerpPosition(m_PositionToMoveTo, PlayerModel.SmoothJumpTime);
            
            m_JumpAllowed = false;
        }
    }

    async void LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = m_transform.position;
        while(time < duration)
        {
            m_transform.position = Vector3.Lerp (startPosition, targetPosition, time/duration);
            time += Time.deltaTime;
            Debug.Log("Time " + time / duration);
            await Task.Yield();
        }
        m_transform.position = targetPosition;
    }

    private void MoveForward()
    {
        m_transform.Translate(Vector3.forward * PlayerModel.MoveSpeed * Time.fixedDeltaTime);
    }
}
