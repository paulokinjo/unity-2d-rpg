using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private  int ComboCounter { get; set; }

    private  float LastTimeAttacked { get; set; }

    private int ComboWindow { get; set; } = 2;

    public PlayerPrimaryAttackState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        XInput = 0;

        if (ComboCounter > 2 || Time.time >= LastTimeAttacked + ComboWindow)
        {
            ComboCounter = 0;
        }

        Player.Animator.SetInteger("ComboCounter", ComboCounter);

        float attackDirection = Player.FacingDirection;
        if (XInput != 0)
        {
            attackDirection = XInput;
        }

        Player.SetVelocity(Player.AttackMovement[ComboCounter].x * attackDirection, Player.AttackMovement[ComboCounter].y);

        StateTimer = .1f;
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0)
        {
            Player.SetVelocity(0, 0);
        }

        if (TriggerCalled)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        Player.StartCoroutine(nameof(Player.BusyFor), .15f);

        LastTimeAttacked = Time.time;
        ComboCounter++;
    }
}
