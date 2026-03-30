using System.Collections;
using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereController : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController controller;
    private PlayerInput playerInput;
    private ParticleSystem shieldEffect;
    private AudioSource audioSource;
    private bool _shieldActive = false;
    private bool _shieldPending = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        shieldEffect = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();

        if (playerInput != null)
        {
            playerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
        }
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput != null && playerInput.actions["Shield"].WasPressedThisFrame())
        {
            _shieldPending = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (playerInput == null || controller == null || Object == null || Object.HasInputAuthority == false)
        {
            return;
        }

        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        var move3D = new Vector3(move.x, 0, move.y);
        controller.Move(move3D * Time.fixedDeltaTime * 5f);

        if (_shieldPending && !_shieldActive)
        {
            _shieldPending = false;
            RPC_ActiveShieldEffect();
        }
        
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.All)]
    void RPC_ActiveShieldEffect()
    {
        StartCoroutine(ShieldEffectCoroutine());
    }

    IEnumerator ShieldEffectCoroutine()
    {
        _shieldActive = true;
        if (shieldEffect != null) shieldEffect.Play();
        if (audioSource != null) audioSource.Play();
        yield return new WaitForSeconds(3f);
        if (shieldEffect != null) shieldEffect.Stop();
        _shieldActive = false;
    }
}
