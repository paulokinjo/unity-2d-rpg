using UnityEngine;
public class SkeletonBattleState : EnemyState
{
    private Transform _player;
    private EnemySkeleton _enemy;
    private int _moveDirection;

    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, EnemySkeleton enemy)
        : base(enemyBase, stateMachine, animationBoolName)
    {
        _enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        _player = GameObject.Find("Player").transform;
    }

    public override void Update()
    {
        base.Update();

        if (_enemy.IsPlayerDetected)
        {
            StateTimer = _enemy.BattleTime;

            if (_enemy.IsPlayerDetected.distance < _enemy.AttackDistance)
            {
                if (CanAttack())
                {
                    StateMachine.ChangeState(_enemy.AttackState);
                }
            }
        }
        else
        {
            if (StateTimer < 0 || Vector2.Distance(_player.transform.position, _enemy.transform.position) > 10)
            {
                StateMachine.ChangeState(_enemy.IdleState);
            }
        }

        _moveDirection = _player.position.x > _enemy.transform.position.x
                       ? 1
                       : -1;

        _enemy.SetVelocity(_enemy.MoveSpeed * _moveDirection, _enemy.Rigidbody2D.velocity.y);
    }

    private bool CanAttack()
    {
        if (Time.time >= _enemy.GetLastTimeAttacked() + _enemy.AttackCoolDown)
        {
            _enemy.SetLastTimeAttacked(Time.time);
            return true;
        }

        return false;
    }
}
