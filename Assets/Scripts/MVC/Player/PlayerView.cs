using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float fallThreshold = -50.0f;

    public LayerMask playerLayer;
    public LayerMask hurdleLayer;
    public ParticleSystem playerParticle;

    private PlayerController playerController { get; set; }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !GameManager.Instance.GamePaused)
            touchStartPos = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && !GameManager.Instance.GamePaused)
        {
            touchEndPos = Input.GetTouch(0).position;

            // Notify swipe if swipe is valid and results in movement.
            if (playerController.SwipeCheck(touchStartPos, touchEndPos))
                ServiceLocator.Get<ObserverSystem>().NotifySwipe();
        }

        //playerController.Simulate(touchStartPos, touchEndPos);
        if(transform.position.y < fallThreshold)
        {
            ServiceLocator.Get<GameManager>().EndGame();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & hurdleLayer) != 0)
        {
            if(!PlayerStateMachine.Instance.activateBoost)
            {
                Instantiate(playerParticle, transform.position, transform.rotation);
                GameManager.Instance.EndGame();
            }                
            else if(PlayerStateMachine.Instance.activateBoost)
            {
                Debug.LogWarning("collided");
                HurdleManager.Instance.RemoveHurdle(collision.gameObject);
            }            
        }
    }

    private void FixedUpdate()
    {
        playerController.Run();
    }

    public Rigidbody GetRigidBody()
    {
        return GetComponent<Rigidbody>();
    }

    internal void SetPlayerController(PlayerController playerController)
    {
        this.playerController = playerController;
        this.playerController.Initialize();
    }
}
