using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private EnemySkeleton _enemy;

    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, EnemySkeleton enemy) 
        : base(enemyBase, stateMachine, animationBoolName)
    {
        _enemy = enemy;
    }

    public override void Update()
    {
        base.Update();

        if (!_enemy.IsKnocked)
        {

            _enemy.SetVelocity(0, 0);
        }

        if (TriggerCalled)
        {
            StateMachine.ChangeState(_enemy.BattleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _enemy.SetLastTimeAttacked(Time.time);   
    }
}
