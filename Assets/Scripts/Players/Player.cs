using System.Collections;
using UnityEngine;
public class Player : Entity
{
    [Header("Attack Info")]
    [SerializeField] private Vector2[] attackMovement;
    [SerializeField] private float _counterAttackDuration = .2f;

    [Header("Move Info")]
    [SerializeField] private float moveSpeed = 12f;
    [SerializeField] private float jumpForce = 12f;

    [Header("Dash Info")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration = 1.5f;
    [SerializeField] private float dashCoolDown = 1.5f;

    private float DashTimer;

    public float DashDirection { get; private set; } = 1f;

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState IdleState { get; private set; }

    public PlayerMoveState MoveState { get; private set; }

    public PlayerJumpState JumpState { get; private set; }

    public PlayerAirState AirState { get; private set; }

    public PlayerDashState DashState { get; private set; }

    public PlayerWallSlideState WallSlideState { get; private set; }

    public PlayerWallJumpState WallJumpState { get; private set; }

    public PlayerPrimaryAttackState PrimaryAttackState { get; private set; }

    public PlayerCounterAttackState CounterAttackState { get; private set; }

    public bool IsBusy { get; private set; }

    public float MoveSpeed => moveSpeed;

    public float JumpForce => jumpForce;

    public float DashSpeed => dashSpeed;

    public float DashDuration => dashDuration;

    public float CounterAttackDuration => _counterAttackDuration;

    public Vector2[] AttackMovement => attackMovement;

    public void AnimationTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();

    public IEnumerator BusyFor(float seconds)
    {
        IsBusy = true;

        yield return new WaitForSeconds(seconds);

        IsBusy = false;
    }

    protected override void Awake()
    {
        StateMachine = new();

        IdleState = new(this, StateMachine, "Idle");
        MoveState = new(this, StateMachine, "Move");
        JumpState = new(this, StateMachine, "Jump");
        AirState = new(this, StateMachine, "Jump");
        DashState = new(this, StateMachine, "Dash");
        WallSlideState = new(this, StateMachine, "WallSlide");
        WallJumpState = new(this, StateMachine, "Jump");
        PrimaryAttackState = new(this, StateMachine, "Attack");
        CounterAttackState = new(this, StateMachine, "CounterAttack");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(IdleState);
    }

    protected override void Update()
    {
        StateMachine.CurrentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        if (IsWallDetected)
        {
            return;
        }

        DashTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && DashTimer < 0)
        {
            DashTimer = dashCoolDown;

            DashDirection = Input.GetAxisRaw("Horizontal");
            if (DashDirection == 0)
            {
                DashDirection = FacingDirection;
            }

            StateMachine.ChangeState(DashState);
        }
    }
}
