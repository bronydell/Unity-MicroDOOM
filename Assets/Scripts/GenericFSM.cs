
public abstract class GenericState
{
    public virtual void Init() {}
    public virtual void Enter() {}
    public virtual void Exit() {}
}

public class GenericFSM
{
    private GenericState currentState;

    public void EnterNewState(GenericState state)
    {
        currentState?.Exit();
        state.Enter();
        currentState = state;
    }
}