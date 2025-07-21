using UnityEngine;

public class AttackStateMachine
{
    public AttackState CurrentState { get; private set; }
    
    public void Initialize(AttackState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(AttackState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
