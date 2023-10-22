using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private PlayerController playerController { get; set; }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            touchStartPos = Input.GetTouch(0).position;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            touchEndPos = Input.GetTouch(0).position;

            // Notify swipe if swipe is valid and results in movement.
            if (playerController.SwipeCheck(touchStartPos, touchEndPos))
                ServiceLocator.Get<ObserverSystem>().NotifySwipe();
        }

        //playerController.Simulate(touchStartPos, touchEndPos);
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
