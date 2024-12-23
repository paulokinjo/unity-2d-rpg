using UnityEngine;

public class EnemySkeleton : Enemy
{
    public SkeletonIdleState IdleState { get; private set; }

    public SkeletonMoveState MoveState { get; private set; }

    public SkeletonBattleState BattleState { get; private set; }

    public SkeletonAttackState AttackState { get; private set; }

    public SkeletonStunnedState StunnedState { get; private set; }

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
        StunnedState = new SkeletonStunnedState(this, StateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.U))
        {
            StateMachine.ChangeState(StunnedState);
        }
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            StateMachine.ChangeState(StunnedState);
            return true;
        }

        return false;
    }
}
