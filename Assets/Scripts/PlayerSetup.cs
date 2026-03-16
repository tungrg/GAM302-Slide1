using Fusion;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void SetupCamera(Transform playerTransform)
    {
        if(Object.HasStateAuthority)
        {
            // Only the client that owns this player object should set up the camera
            CameraFollow cameraFollow = FindObjectOfType<CameraFollow>();
            if (cameraFollow != null)
            {
                cameraFollow.AssignCamera(playerTransform);
            }
            else
            {
                Debug.LogError("CameraFollow script not found in the scene.");
            }
        }
    }
}
