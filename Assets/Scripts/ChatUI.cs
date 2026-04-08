using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChatUI : MonoBehaviour
{
    public TMPro.TMP_InputField chatInput;
    public Button sendButton;
    public TMPro.TextMeshProUGUI chatContext;


    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    public void OnSendButtonClicked()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            ChatManager.Instance.SendMessage(message);
            chatInput.text = "";
        }
    }

    public RectTransform contentRect;
    public RectTransform viewportRect;

    public void ScrollToBottom()
    {
        StartCoroutine(ScrollToBottomNextFrame());
    }

    private IEnumerator ScrollToBottomNextFrame()
    {
        // Wait one frame so the layout rebuilds with the new text first
        yield return null;
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentRect);
        float overflow = contentRect.rect.height - viewportRect.rect.height;
        if (overflow > 0f)
        {
            contentRect.anchoredPosition = new Vector2(contentRect.anchoredPosition.x, overflow);
        }
    }
}
