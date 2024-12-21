
public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine playerStateMachine, string animBoolName) 
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        Player.SetVelocity(XVelocity, Player.JumpForce);
    }

    public override void Update()
    {
        base.Update();

        if (YVelocity < 0)
        {
            StateMachine.ChangeState(Player.AirState);
        }
    }
}
