using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerProperties : NetworkBehaviour
{
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int health { get; set; }

    private const int MaxHealth = 100;
    private HealthBar healthBar;

    private void OnHealthChanged()
    {
        healthBar.UpdateHealthBar(MaxHealth, health);
    }
    void Awake()
    {
        healthBar = GetComponentInChildren<HealthBar>();
    }

    public override void Spawned()
    {
        health = 50;
        healthBar.UpdateHealthBar(MaxHealth, health);
    }


    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RPC_TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }
}
