using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Componentes")]
    [SerializeField] private Player player;
    [SerializeField] private Animator anim;
    
    [Header("Configurações de Animação")]
    [SerializeField] private int idleAnimationId = 0;
    [SerializeField] private int walkAnimationId = 1;
    
    private void Start()
    {
        // Busca componentes se não foram atribuídos
        if (player == null)
            player = GetComponent<Player>();
            
        if (anim == null)
            anim = GetComponent<Animator>();
            
        // Validação de componentes
        if (player == null)
        {
            Debug.LogError($"[{nameof(PlayerAnimation)}] Componente Player não encontrado!");
            enabled = false;
            return;
        }
        
        if (anim == null)
        {
            Debug.LogError($"[{nameof(PlayerAnimation)}] Componente Animator não encontrado!");
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
    
    /// <summary>
    /// Atualiza a animação baseada no movimento do jogador
    /// </summary>
    private void UpdateAnimation()
    {
        bool isMoving = player.Direction.sqrMagnitude > 0;
        int animationId = isMoving ? walkAnimationId : idleAnimationId;
        anim.SetInteger("transition", animationId);
    }
    
    /// <summary>
    /// Atualiza a rotação do sprite baseada na direção do movimento
    /// </summary>
    private void UpdateRotation()
    {
        Vector2 direction = player.Direction;
        
        if (direction.x > 0)
        {
            // Olhando para a direita
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (direction.x < 0)
        {
            // Olhando para a esquerda
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        // Se direction.x == 0, mantém a rotação atual
    }
}
