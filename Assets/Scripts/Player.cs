using UnityEngine;
using UnityEngine.InputSystem; // Necessário para usar o novo Input System

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rig;
    private Vector2 direction;

    private PlayerInputActions inputActions;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Enable();

        // Escuta quando a ação de movimento é executada
        inputActions.Player.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => direction = Vector2.zero;
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);
    }
}
