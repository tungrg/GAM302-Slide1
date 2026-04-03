using Fusion;
using UnityEngine;
using UnityEngine.UI;

public class MatchTimer : NetworkBehaviour
{
    public TMPro.TMP_Text timerText;
    [Networked] 
    private float remainingTime { get; set; }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            remainingTime = 300f; // Set the match duration to 5 minutes
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (Object.HasStateAuthority)
        {
            remainingTime -= Runner.DeltaTime;
            if (remainingTime < 0)
            {
                remainingTime = 0;
            }
        }
    }

    public override void Render()
    {
        UpdateTimerText();
    }
    void UpdateTimerText()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
