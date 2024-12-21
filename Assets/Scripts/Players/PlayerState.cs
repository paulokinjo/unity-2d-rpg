using UnityEngine;

public class PlayerState
{
    protected Player Player { get; }

    protected PlayerStateMachine StateMachine { get; }

    protected float XInput { get; set; }

    protected float YInput { get; set; }

    protected float StateTimer { get; set; }

    protected bool TriggerCalled { get; set; }

    public string AnimBoolName { get; }

    public PlayerState(Player player, PlayerStateMachine playerStateMachine, string animBoolName)
    {
        Player = player;
        StateMachine = playerStateMachine;
        AnimBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        Debug.Log($"I Enter {AnimBoolName}");
        Player.Animator.SetBool(AnimBoolName, true);

        TriggerCalled = false;
    }

    public virtual void Update()
    {
        XInput = Input.GetAxisRaw("Horizontal");
        YInput = Input.GetAxisRaw("Vertical");
        
        Player.Animator.SetFloat("yVelocity", YVelocity);

        if (StateTimer >= 0)
        {
            StateTimer -= Time.deltaTime;
        }
    }

    public virtual void Exit()
    {
        Debug.Log($"I Exit {AnimBoolName}");
        Player.Animator.SetBool(AnimBoolName, false);
    }

    public virtual void AnimationFinishTrigger() => TriggerCalled = true;

    protected float YVelocity => Player.Rigidbody2D.velocity.y;

    protected float XVelocity => Player.Rigidbody2D.velocity.x;
}
