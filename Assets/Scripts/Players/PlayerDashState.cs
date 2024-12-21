using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        StateTimer = Player.DashDuration;
    }

    public override void Update()
    {
        base.Update();

        if (!Player.IsGroundDetected && Player.IsWallDetected)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }
        else
        {
            const float doNotFallVelocity = 0;
            Player.SetVelocity(Player.DashSpeed * Player.DashDirection, doNotFallVelocity);

            if (StateTimer <= 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        Player.SetVelocity(0, YVelocity);
    }
}
