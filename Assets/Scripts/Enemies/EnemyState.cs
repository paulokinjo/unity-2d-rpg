using UnityEngine;

public class EnemyState
{
    private string _animationBoolName;

    protected EnemyStateMachine StateMachine { get; private set; }

    protected Enemy EnemyBase { get; private set; }

    protected Rigidbody2D Rigidbody2D { get; private set; }

    protected bool TriggerCalled { get; private set; }

    protected float StateTimer { get; set; }

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName)
    {
        EnemyBase = enemyBase;
        StateMachine = stateMachine;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        TriggerCalled = false;
        EnemyBase.Animator.SetBool(_animationBoolName, true);
        Rigidbody2D = EnemyBase.Rigidbody2D;
    }

    public virtual void Update()
    {
        StateTimer -= Time.deltaTime;
    }

    public virtual void Exit()
    {
        EnemyBase.Animator.SetBool(_animationBoolName, false);
    }

    public void AnimationFinishTrigger() => TriggerCalled = true;
}
