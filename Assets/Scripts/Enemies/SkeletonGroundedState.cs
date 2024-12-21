using UnityEngine;

public class SkeletonGroundedState : EnemyState
{
    protected EnemySkeleton Enemy { get; private set; }
    
    protected Transform Player { get; private set; }

    public SkeletonGroundedState(Enemy enemyBase, EnemyStateMachine stateMachine, string animBoolName, EnemySkeleton enemy)
        : base(enemyBase, stateMachine, animBoolName)
    {
        Enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        Player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        if (Enemy.IsPlayerDetected || Vector2.Distance(Enemy.transform.position, Player.transform.position) < 2)
        {
            StateMachine.ChangeState(Enemy.BattleState);
        }
    }
}
