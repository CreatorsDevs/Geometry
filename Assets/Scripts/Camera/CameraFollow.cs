using UnityEngine;

public class CameraFollow : Singleton<CameraFollow>
{
    private Transform player; // Reference to player's transform
    public Vector3 offset; // Offset in local space relative to the player
    public Vector3 rotationOffset; // Degrees to offset the rotation

    protected override void Awake()
    {
        base.Awake();
        ServiceLocator.Register(this);
    }

    private void LateUpdate() // Using LateUpdate for smoother camera movement after all player movements are processed
    {
        if (player && player.gameObject.activeInHierarchy)
        {
            // Calculate the world space offset based on the player's current rotation
            Vector3 worldOffset = player.TransformVector(offset);

            // Update the camera's position using the calculated offset
            transform.position = player.position + worldOffset;

            // Assuming the camera should also look at the player
            transform.LookAt(player);

            // Apply rotation offset
            transform.eulerAngles += rotationOffset;
        }
    }

    // Method to assign a new player transform to the camera
    public void SetPlayer(Transform newPlayer)
    {
        player = newPlayer;
    }
}
