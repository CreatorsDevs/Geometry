using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float m_MoveSpeed = 10.0f;
    [Tooltip("In Degrees")]
    [SerializeField] private float m_AngleThresholdForY = 30;
    private Vector2 m_TouchStartPos;
    private Vector2 m_TouchEndPos;
    private Vector3 m_PositionToMoveTo;
    private bool m_JumpAllowed = false;
    private bool m_CanMoveRight = false;
    private bool m_CanMoveLeft = false;
    private Rigidbody rb;

    public float moveDistance = 2.0f;
    public float jumpVelocity = 7.0f;
    public float smoothTime = 0.2f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        SwipeCheck();
    }

    private void FixedUpdate()
    {
        MoveForward();
        MoveLeft();
        MoveRight();
        Jump();
    }

    private void SwipeCheck()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            m_TouchStartPos = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            m_TouchEndPos = Input.GetTouch(0).position;
            Vector2 swipeDelta = m_TouchEndPos - m_TouchStartPos;

            Vector2 inputDirection = swipeDelta.normalized;
            Vector2 baseVector = m_TouchEndPos.x <= m_TouchStartPos.x ? Vector2.left : Vector2.right;
            float relation = Vector2.Dot(baseVector, inputDirection);
            bool isInputInY = relation <= Mathf.Cos(Mathf.Deg2Rad * m_AngleThresholdForY);

            if (isInputInY && (m_TouchEndPos.y > m_TouchStartPos.y) && rb.velocity.y == 0)
                { m_JumpAllowed = true; }
            if ((Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) && (swipeDelta.x > 0) && (rb.velocity.x == 0))
                { m_CanMoveRight = true; }
            if ((Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) && (swipeDelta.x < 0) && (rb.velocity.x == 0))
                { m_CanMoveLeft = true; }
        }
    }

    private void MoveRight()
    {
        if(m_CanMoveRight)
        {
            m_PositionToMoveTo = transform.position + new Vector3(moveDistance, 0, 0);
            LerpPosition(m_PositionToMoveTo, smoothTime);
            m_CanMoveRight = false;
        }
    }

    private void MoveLeft()
    {
        if(m_CanMoveLeft)
        {
            m_PositionToMoveTo = transform.position + new Vector3(-moveDistance, 0, 0);
            LerpPosition(m_PositionToMoveTo, smoothTime);
            m_CanMoveLeft = false;
        }
    }

    private void Jump()
    {
        if(m_JumpAllowed)
        {
            m_PositionToMoveTo = transform.position + new Vector3(0, moveDistance + 1.5f, 0);
            LerpPosition(m_PositionToMoveTo, smoothTime + 0.1f);
            
            m_JumpAllowed = false;
        }
    }

    async void LerpPosition(Vector3 targetPosition, float duration)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while(time < duration)
        {
            transform.position = Vector3.Lerp (startPosition, targetPosition, time/duration);
            time += Time.deltaTime;
            await Task.Yield();
        }
        transform.position = targetPosition;
    }

    private void MoveForward()
    {
        transform.Translate(Vector3.forward * moveDistance * Time.fixedDeltaTime);
    }
}
