using UnityEngine;
using UnityEngine.UI;
using Fusion;

public class Chat : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public TMPro.TMP_InputField inputField;
    public GameObject Content;
    public GameObject messagePrefab;
    
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_SendMessage(string message)
    {
        var msg = Instantiate(messagePrefab, Content.transform);
        msg.GetComponentInChildren<TMPro.TMP_Text>().text = message;
    }
    public void SendMessage()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }

        if (Object == null || Object.IsValid == false)
        {
            Debug.LogError("Chat NetworkObject is not initialized. Make sure this GameObject has a NetworkObject component and is spawned.");
            return;
        }

        RPC_SendMessage(inputField.text);
        inputField.text = "Enter text...";
    }
}
