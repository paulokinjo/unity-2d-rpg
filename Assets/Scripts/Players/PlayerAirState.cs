public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Player.IsGroundDetected)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        else if(Player.IsWallDetected)
        {
            StateMachine.ChangeState(Player.WallSlideState);
        }

        if (XInput != 0)
        {
            Player.SetVelocity(Player.MoveSpeed * .8f * XInput, YVelocity);
        }
    }
}
