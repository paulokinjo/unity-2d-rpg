public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetVelocity(0, 0);
    }

    public override void Update()
    {
        base.Update();

        if (XInput == Player.FacingDirection && Player.IsWallDetected)
        {
            return;
        }

        if (XInput != 0 && !Player.IsBusy)
        {
            StateMachine.ChangeState(Player.MoveState);
        }
    }
}
