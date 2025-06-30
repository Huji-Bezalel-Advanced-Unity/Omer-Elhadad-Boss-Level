using UnityEngine;

public class SmallAttackState : AttackState
{
    public SmallAttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController) : 
        base(container, stateMachine, attackData, animBoolName, stateAnimatorController)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!Container.InputHandler.SwapInput || Container.InputHandler.ShootInput) return;
        Container.InputHandler.ResetSwapInput();
        StateMachine.ChangeState(Container.LargeAttackState);
    }
}
