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

    [Header("Stunned info")]
    [SerializeField] private float _stunDuration;
    [SerializeField] private Vector2 _stunDirection;
    [SerializeField] private bool _canBeStunned;
    [SerializeField] private GameObject _counterImage;

    [HideInInspector] protected float LastTimeAttacked { get; set; }

    public float MoveSpeed => _moveSpeed;

    public float IdleTime => _idleTime;

    public float BattleTime => _battleTime;

    public float AttackDistance => _attackDistance;

    public Vector2 StunDirection => _stunDirection;

    public float StunDuration => _stunDuration;

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

    public virtual void OpenCounterAttackWindow()
    {
        _canBeStunned = true;
        _counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        _canBeStunned = false;
        _counterImage.SetActive(false);
    }

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

    public virtual bool CanBeStunned()
    {
        if (_canBeStunned)
        {
            CloseCounterAttackWindow();
            return true;
        }

        return false;
    }
}
