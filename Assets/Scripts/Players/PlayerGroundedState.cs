using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
        : base(player, playerStateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space) && Player.IsGroundDetected)
        {
            StateMachine.ChangeState(Player.JumpState);
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StateMachine.ChangeState(Player.PrimaryAttackState);
        }

        if(!Player.IsGroundDetected)
        {
            StateMachine.ChangeState(Player.AirState);
        }
    }
}
