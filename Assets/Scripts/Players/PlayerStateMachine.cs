public class PlayerStateMachine
{ 
    public PlayerState CurrentState { get; private set; }

    public void Initialize(PlayerState playerState)
    {  
        CurrentState = playerState;
        CurrentState.Enter();
    }

    public  void ChangeState(PlayerState playerState)
    {
        CurrentState.Exit();
        Initialize(playerState);
    }
}
