public class SkeletonIdleState : SkeletonGroundedState
{

    public SkeletonIdleState(
        Enemy enemyBase,
        EnemyStateMachine stateMachine, 
        string animationBoolName,
        EnemySkeleton enemy)
        : base(enemyBase, stateMachine, animationBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateTimer = Enemy.IdleTime;
    }

    public override void Update()
    {
        base.Update();
        if (StateTimer < 0)
        {
            StateMachine.ChangeState(Enemy.MoveState);
        }      
    }
}
