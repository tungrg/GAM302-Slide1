using Fusion;
using UnityEngine;

public class XuLyVaCham : NetworkBehaviour
{
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (!hit.gameObject.CompareTag("Player"))
            return;

        NetworkObject otherNetObj = hit.gameObject.GetComponentInParent<NetworkObject>();
        if (otherNetObj == null || otherNetObj == Object)
            return;

        // Tell the other player's client (StateAuthority) to despawn itself
        var otherPlayer = otherNetObj.GetComponent<XuLyVaCham>();
        if (otherPlayer != null)
        {
            otherPlayer.RPC_Despawn();
        }
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_Despawn()
    {
        if (Object != null)
            Runner.Despawn(Object);
    }
}
