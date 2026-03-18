using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int health { get; set; }

    public Slider healthSlider;
    public PlayerInput playerInput;
    private void OnHealthChanged()
    {
        healthSlider.value = health;
    }

    public override void Spawned()
    {
        healthSlider.maxValue = 100;
        health = 50;
        playerInput = GetComponent<PlayerInput>();
    }
    // Update is called once per frame  
    public override void FixedUpdateNetwork()
    {
        if (playerInput.actions["Jump"].triggered)
        {
            Debug.Log("Jump action triggered, reducing health by 10.");
            health -= 10;
        }
    }
}
