
using UnityEngine;

public abstract class StateBase
{
    protected StateMachineBase stateMachineBase;
    protected MonoBehaviour context;

    protected StateBase(StateMachineBase stateMachineBase, MonoBehaviour context)
    {
        this.stateMachineBase = stateMachineBase;
        this.context = context;
    }

    public virtual void OnEnter() => Debug.Log($"StateMachine state {GetType()} OnEnter()");
    public virtual void OnUpdate() { }
    public virtual void OnExit() { }
    public virtual void OnDrawGizmo() { }
}
