using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    [SerializeField]
    private GameObject playerPrefab;
    public void PlayerJoined(PlayerRef player)
    {
        if(player==Runner.LocalPlayer)
        {
            Runner.Spawn(playerPrefab, Vector3.zero, Quaternion.identity, Runner.LocalPlayer, (runner, obj) =>
            {
                // This callback is called on the client that owns the player object after it has been spawned
                var playerSetup = obj.GetComponent<PlayerSetup>();
                if (playerSetup != null)                {
                    playerSetup.SetupCamera(obj.transform);
                }
                else
                {
                    Debug.LogError("PlayerSetup component not found on the spawned player object.");
                }
            });
            
        }
    }
}
