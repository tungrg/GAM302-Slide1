using Fusion;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereController : NetworkBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private CharacterController controller;
    private PlayerInput playerInput;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();

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
    }
}
