using Unity.VisualScripting;

public class PlayerWallJumpState : PlayerState
{
    public PlayerWallJumpState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateTimer = .4f;

        Player.SetVelocity(5 * -Player.FacingDirection, Player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (StateTimer < 0) 
        {
            StateMachine.ChangeState(Player.AirState);
        }
        else if (Player.IsGroundDetected && XInput == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }

    }
}
