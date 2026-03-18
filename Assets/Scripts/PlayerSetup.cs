using Fusion;
using UnityEngine;

public class PlayerSetup : NetworkBehaviour
{
    protected virtual bool CheckStateAuthority() => Object.HasStateAuthority;

    public void SetupCamera(Transform playerTransform)
    {
        if(CheckStateAuthority())
        {
            // Only the client that owns this player object should set up the camera
            CameraFollow cameraFollow = FindFirstObjectByType<CameraFollow>();
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
