public class SkeletonMoveState : SkeletonGroundedState
{
    public SkeletonMoveState(
        Enemy enemyBase,
        EnemyStateMachine stateMachine,
        string animationBoolName,
        EnemySkeleton enemy
        )
        : base(enemyBase, stateMachine, animationBoolName, enemy)
    {
    }

    public override void Update()
    {
        base.Update();

        var velocityX = Enemy.MoveSpeed * Enemy.FacingDirection;
        
        Enemy.SetVelocity(velocityX, Rigidbody2D.velocity.y);

        if (Enemy.IsWallDetected || !Enemy.IsGroundDetected)
        {
            Enemy.Flip();
            StateMachine.ChangeState(Enemy.IdleState);
        }
    }
}
