using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateTimer = Player.CounterAttackDuration;
        Player.Animator.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Update()
    {
        base.Update();
        Player.SetVelocity(0, 0);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(Player.AttackCheck.position, Player.AttackCheckRadius);

        foreach (var hit in colliders)
        {
            Enemy enemy = hit.GetComponent<Enemy>();    
            if (enemy != null)
            {
                if(enemy.CanBeStunned())
                {
                    StateTimer = 10; // any value bigger than 1
                    Player.Animator.SetBool("SuccessfulCounterAttack", true);
                }
            }
        }

        if (StateTimer < 0 || TriggerCalled) 
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }
}
