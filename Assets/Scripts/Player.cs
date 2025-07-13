using UnityEngine;
using UnityEngine.InputSystem; // Necessário para usar o novo Input System

public class Player : MonoBehaviour
{
    [Header("Configurações")]
    [SerializeField] private float speed = 5f;
    
    private Rigidbody2D rig;
    private Vector2 _direction;
    private PlayerInputActions inputActions;

    // Property melhorada com set privado para controle
    public Vector2 Direction 
    { 
        get { return _direction; } 
        private set { _direction = value; } 
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions?.Enable();

        // Escuta quando a ação de movimento é executada
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnDisable()
    {
        inputActions?.Disable();
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        
        // Validação de componente
        if (rig == null)
        {
            Debug.LogError($"[{nameof(Player)}] Rigidbody2D não encontrado!");
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        if (rig != null)
        {
            rig.MovePosition(rig.position + Direction * speed * Time.fixedDeltaTime);
        }
    }
    
    // Callbacks separados para melhor organização
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        Direction = Vector2.zero;
    }
    
    // Método público para parar o movimento
    public void StopMovement()
    {
        Direction = Vector2.zero;
    }
}
