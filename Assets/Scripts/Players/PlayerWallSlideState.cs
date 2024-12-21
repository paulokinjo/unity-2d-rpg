using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StateMachine.ChangeState(Player.WallJumpState);
        }
        else
        {
            if (XInput != 0 && Player.FacingDirection != XInput)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (Player.IsGroundDetected)
            {
                StateMachine.ChangeState(Player.IdleState);
            }

            if (YInput < 0)
            {
                Player.SetVelocity(0, YVelocity);
            }
            else
            {
                Player.SetVelocity(0, YVelocity * .7f);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
