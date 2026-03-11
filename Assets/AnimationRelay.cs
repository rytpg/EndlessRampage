using UnityEngine;

public class AnimationRelay : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    void Awake()
    {
        if (player == null)
        {
            player = GetComponentInParent<PlayerController>();
        }
    }

    public void SwordAttack() => player.SwordAttack();
    public void EndSwordAttack() => player.EndSwordAttack();
    public void SwordAttackHeavy() => player.SwordAttackHeavy();
}
