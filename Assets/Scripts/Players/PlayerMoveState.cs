public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        Player.SetVelocity(XInput * Player.MoveSpeed, YVelocity);

        if (XInput == 0 || Player.IsWallDetected)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
    }
}
