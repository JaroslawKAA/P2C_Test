using System;

[Serializable]
public abstract class StateMachineBase
{
    StateBase currentState;
    
    public StateBase CurrentState
    {
        get => currentState;
        private set
        {
            if(currentState != null)
                currentState.OnExit();
            
            if (value != null) 
                value.OnEnter();
            
            currentState = value;
            
            onStateChanged?.Invoke(currentState);
        }
    }

    public event Action<StateBase> onStateChanged;

    public void TransitionTo(StateBase nextState)
    {
        CurrentState = nextState;
    }

    public void Update()
    {
        if(currentState != null)
            currentState.OnUpdate();
    }

    public void OnDrawGizmos()
    {
        if (currentState != null) 
            currentState.OnDrawGizmo();
    }
}