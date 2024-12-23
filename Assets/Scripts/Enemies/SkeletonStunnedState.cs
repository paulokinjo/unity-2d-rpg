using UnityEngine;

public class SkeletonStunnedState : SkeletonGroundedState
{
    public SkeletonStunnedState(
        Enemy enemyBase,
        EnemyStateMachine stateMachine,
        string animationBoolName,
        EnemySkeleton enemy
        )
        : base(enemyBase, stateMachine, animationBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Enemy.EntityFX.InvokeRepeating("RedColorBlink", 0, .1f);
        
        StateTimer = Enemy.StunDuration;

        Rigidbody2D.velocity = new Vector2(-Enemy.FacingDirection * Enemy.StunDirection.x, Enemy.StunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            StateMachine.ChangeState(Enemy.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        Enemy.EntityFX.Invoke("CancelRedBlink", 0);
    }
}
