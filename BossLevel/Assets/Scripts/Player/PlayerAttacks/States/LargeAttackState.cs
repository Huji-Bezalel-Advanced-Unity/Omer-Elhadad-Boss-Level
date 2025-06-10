using UnityEngine;

public class LargeAttackState : AttackState
{
    public LargeAttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController) : 
        base(container, stateMachine, attackData, animBoolName, stateAnimatorController)
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
        if (!Container.InputHandler.SwapInput) return;
        Container.InputHandler.ResetSwapInput();
        StateMachine.ChangeState(Container.SmallAttackState);
    }

    public override void Exit()
    {
        base.Exit();
        // Additional logic specific to SmallAttackState can be added here
        Debug.Log("Exiting SmallAttackState");
    }
    
    
}