using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public PlayerType PlayerType { get; private set; }
    public float MoveSpeed { get; set; }
    public float JumpHeight { get; private set; }
    public float MoveDistance { get; private set; }
    public float SmoothSwipeTime { get; private set; }
    public float SmoothJumpTime { get; private set; }

    public PlayerModel(PlayerScriptableObject playerScriptableObject)
    {
        PlayerType = playerScriptableObject.playerType;
        MoveSpeed = playerScriptableObject.moveSpeed;
        JumpHeight = playerScriptableObject.jumpHeight;
        MoveDistance = playerScriptableObject.moveDistance;
        SmoothSwipeTime = playerScriptableObject.smoothSwipeTime;
        SmoothJumpTime = playerScriptableObject.smoothJumpTime;
    }
}
