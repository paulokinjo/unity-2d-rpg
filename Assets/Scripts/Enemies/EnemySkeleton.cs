public class EnemySkeleton : Enemy
{
    public SkeletonIdleState IdleState { get; private set; }

    public SkeletonMoveState MoveState { get; private set; }

    public SkeletonBattleState BattleState { get; private set; }

    public SkeletonAttackState AttackState { get; private set; }

    public void SetLastTimeAttacked(float time)
    {
        LastTimeAttacked = time;
    }

    protected override void Awake()
    {
        base.Awake();

        IdleState = new SkeletonIdleState(this, StateMachine, "Idle", this);
        MoveState = new SkeletonMoveState(this, StateMachine, "Move", this);
        BattleState = new SkeletonBattleState(this, StateMachine, "Move", this);
        AttackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }
}
