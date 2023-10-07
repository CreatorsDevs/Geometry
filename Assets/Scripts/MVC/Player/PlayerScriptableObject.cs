using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{
    public PlayerType playerType;
    public float moveSpeed;
    public float jumpHeight;
    public float moveDistance;
    public float smoothSwipeTime;
    public float smoothJumpTime;
    public PlayerView playerView;
}
