using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected bool IsAbilityDone;
    public PlayerAbilityState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsAbilityDone) return;
        
        if (!PlayerController.CheckIfGrounded())
        {
            StateMachine.ChangeState(PlayerController.InAirState);
        }
        else
        {
            StateMachine.ChangeState(PlayerController.IdleState);
        }
        
    }
    
    public override void Exit()
    {
        base.Exit();
        IsAbilityDone = true;
    }
}
