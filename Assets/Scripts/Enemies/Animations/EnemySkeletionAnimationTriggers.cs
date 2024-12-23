using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class EnemySkeletionAnimationTriggers : MonoBehaviour
{
    private EnemySkeleton _enemy => GetComponentInParent<EnemySkeleton>();

    private void AnimationTrigger() => _enemy.AnimationFinishTrigger();

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_enemy.AttackCheck.position, _enemy.AttackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
            {
                hit.GetComponent<Player>().Damage();
            }
        }
    }

    private void OpenCounterAttackWindow() => _enemy?.OpenCounterAttackWindow();

    private void CloseCounterAttackWindow() => _enemy.CloseCounterAttackWindow();
}
