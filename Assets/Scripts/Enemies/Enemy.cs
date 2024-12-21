using UnityEditor.Media;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] private LayerMask _whatIsPlayer;

    [Header("Move Info")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _idleTime;
    [SerializeField] private float _battleTime;

    [Header("Attack Info")]
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _attackCooldown;
    [HideInInspector] protected float LastTimeAttacked { get; set; }

    public float MoveSpeed => _moveSpeed;

    public float IdleTime => _idleTime;

    public float BattleTime => _battleTime;

    public float AttackDistance => _attackDistance;

    public EnemyStateMachine StateMachine { get; private set; }

    public virtual RaycastHit2D IsPlayerDetected =>
        Physics2D.Raycast(
            origin: WallCheck.position,
            direction: Vector2.right * FacingDirection,
            distance: 50,
            layerMask: _whatIsPlayer);

    public float AttackCoolDown => _attackCooldown;

    public void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public float GetLastTimeAttacked() => LastTimeAttacked;

    protected override void Awake()
    {
        StateMachine = new EnemyStateMachine();
    }

    protected override void Update()
    {
        StateMachine.CurrentState.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackDistance * FacingDirection, transform.position.y));
    }
}
