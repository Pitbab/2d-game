
public class StateMachine
{
    public PlayerStates currentState;
    
    public void Initialize(PlayerStates startingState)
    {
        currentState = startingState;
        startingState.Enter();
    }

    public void ChangeState(PlayerStates newState)
    {
        currentState.Exit();
        currentState = newState;
        newState.Enter();
    }


}
