using UnityEngine;
using Photon.Chat;
using ExitGames.Client.Photon;
using Fusion;
using System.Collections.Generic;

public class ChatManager : NetworkBehaviour
{
    public static ChatManager Instance;

    private List<string> chatMessages = new List<string>();

    public ChatUI chatUI;
    void Awake(){
        Instance = this;
    }
    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_ReceiveMessage(string message)
    {
        chatMessages.Add(message);
        chatUI.chatContext.text += message + "\n";
        chatUI.ScrollToBottom();
    }
    public void SendMessage(string message)
    {
        string formattedMessage = $"{Runner.LocalPlayer.PlayerId}: {message}";
        RPC_ReceiveMessage(formattedMessage);
    }
}
