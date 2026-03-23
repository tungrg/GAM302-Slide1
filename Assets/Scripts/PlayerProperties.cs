using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int health { get; set; }

    private const int MaxHealth = 100;

    [SerializeField]
    private HealthBar healthBar;
    private PlayerInput playerInput;

    private void OnHealthChanged()
    {
        healthBar.UpdateHealthBar(MaxHealth, health);
    }
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

         if (playerInput != null)
        {
            playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        }
    }

    public override void Spawned()
    {
        health = 50;
        healthBar.UpdateHealthBar(MaxHealth, health);
    }

    // Update is called once per frame  
    public override void FixedUpdateNetwork()
    {
        if (playerInput == null || Object == null || Object.HasInputAuthority == false)
        {
            return;
        }

        if (playerInput.actions["Jump"].triggered)
        {
            Debug.Log("Jump action triggered, reducing health by 10.");
            health = Mathf.Max(0, health - 10);
        }
    }
}
