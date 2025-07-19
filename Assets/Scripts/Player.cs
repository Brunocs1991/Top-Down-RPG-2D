using UnityEngine;
using UnityEngine.InputSystem; // Necessário para usar o novo Input System

public class Player : MonoBehaviour
{
    #region Variables
    [Header("Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;

    private float initialSpeed;
    private Rigidbody2D rig;
    private Vector2 _direction;
    private bool _isRunning;
    private bool _isRolling;
    private PlayerInputActions inputActions;
    #endregion

    #region Properties
    public Vector2 Direction 
    { 
        get { return _direction; } 
        private set { _direction = value; } 
    }
    public bool IsRunning
    {
        get { return _isRunning; }
        private set { _isRunning = value; }
    }
    public bool IsRolling
    {
        get { return _isRolling; }
        private set { _isRolling = value; }
    }
    #endregion

    #region Unity Methods
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        EnableInputActions();
    }

    private void OnDisable()
    {
        inputActions?.Disable();
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initialSpeed = speed;
        if (rig == null)
        {
            Debug.LogError($"[{nameof(Player)}] Rigidbody2D not found!");
            enabled = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    #endregion

    #region Movement
    public void StopMovement()
    {
        Direction = Vector2.zero;
    }

    private void OnMoveAction()
    {
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
    }

    private void OnSprintAction()
    {
        inputActions.Player.Sprint.performed += OnSprintPerformed;
        inputActions.Player.Sprint.canceled += OnSprintCanceled;
    }

    private void OnRollingAction()
    {
        inputActions.Player.Roll.performed += OnRollingPerformed;
        inputActions.Player.Roll.canceled += OnRollingCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Direction = context.ReadValue<Vector2>();
    }
    
    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        Direction = Vector2.zero;
    }

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        speed = runSpeed;
        IsRunning = true;
    }
    
    private void OnSprintCanceled(InputAction.CallbackContext context)
    {
        speed = initialSpeed;
        IsRunning = false;
    }

    private void OnRollingPerformed(InputAction.CallbackContext context)
    {
        IsRolling = true;
    }
    
    private void OnRollingCanceled(InputAction.CallbackContext context)
    {
        IsRolling = false;
    }

    private void MovePlayer()
    {
        if (rig != null)
        {
            rig.MovePosition(rig.position + Direction * speed * Time.fixedDeltaTime);
        }
    }

    private void EnableInputActions()
    {
        inputActions?.Enable();
        OnMoveAction();
        OnSprintAction();
        OnRollingAction();
    }
    #endregion
}
