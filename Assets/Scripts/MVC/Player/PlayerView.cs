using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour //, IHit
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
            playerController.SwipeCheck(touchStartPos, touchEndPos);
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
