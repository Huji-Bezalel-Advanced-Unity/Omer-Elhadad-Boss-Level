using UnityEngine;

public class LargeAttackState : AttackState
{
    public LargeAttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName) : 
        base(container, stateMachine, attackData, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        // Additional logic specific to SmallAttackState can be added here
        Debug.Log("Entering SmallAttackState");
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic specific to SmallAttackState can be added here
        Debug.Log("Exiting SmallAttackState");
    }
    
    
}