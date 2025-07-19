using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    #region Variables
    [Header("Components")]
    [SerializeField] private Player player;
    [SerializeField] private Animator anim;
    
    [Header("Animation Settings")]
    [SerializeField] private int idleAnimationId = 0;
    [SerializeField] private int walkAnimationId = 1;
    [SerializeField] private int runAnimationId = 2;

    #endregion

    #region Unity Methods
    private void Start()
    {
        if (player == null)
            player = GetComponent<Player>();
        if (anim == null)
            anim = GetComponent<Animator>();
        if (player == null)
        {
            Debug.LogError($"[{nameof(PlayerAnimation)}] Player component not found!");
            enabled = false;
            return;
        }
        if (anim == null)
        {
            Debug.LogError($"[{nameof(PlayerAnimation)}] Animator component not found!");
            enabled = false;
            return;
        }
    }

    private void Update()
    {
        if (player == null || anim == null) return;
        UpdateAnimation();
        UpdateRotation();
    }
    #endregion

    #region Animation Logic
    private void UpdateAnimation()
    {
        if (player == null || anim == null)
            return;

        float movement = player.Direction.sqrMagnitude;

        // Rolling tem prioridade quando está se movendo
        if (movement > 0f && player.IsRolling)
        {
            OnRoll();
            return; // Não executa outras animações enquanto estiver rolando
        }

        // Se não está rolando, executa as animações normais
        OnMove();
        OnRun();
    }

    private void OnMove()
    {
        float movement = player.Direction.sqrMagnitude;
        int animationId;

        if (movement == 0f)
        {
            animationId = idleAnimationId;
        }
        else
        {
            animationId = walkAnimationId;
        }

        anim.SetInteger("transition", animationId);
    }

    private void OnRun()
    {
        float movement = player.Direction.sqrMagnitude;
        if (player.IsRunning && movement > 0f)
        {
            anim.SetInteger("transition", runAnimationId);
        }
    }

    private void OnRoll()
    {
        anim.SetTrigger("isRoll");
    }

    private void UpdateRotation()
    {
        Vector2 direction = player.Direction;
        if (direction.x > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }
    #endregion
}
