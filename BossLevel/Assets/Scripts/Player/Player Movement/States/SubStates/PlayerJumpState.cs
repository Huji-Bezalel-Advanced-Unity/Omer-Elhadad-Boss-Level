using System;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public static event Action PlayerJump; 

    public PlayerJumpState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        // Trigger the jump event
        PlayerJump?.Invoke();
        
        base.Enter();
        PlayerController.PlayerRigidbody.gravityScale = PlayerData.baseGravityScale; // Set gravity scale for jump
        
        PlayerController.SetYVelocity(PlayerData.jumpSpeed);
        IsAbilityDone = true;
    }
    
    public override void LogicUpdate()
    {
        if (PlayerController.InputHandler.DashInput && PlayerController.DashState.CanDash())
        {
            PlayerController.InputHandler.ResetDashInput();
            StateMachine.ChangeState(PlayerController.DashState);
        }
        
        if (!(PlayerController.CurrentVelocity.y < 0)) return;
        base.LogicUpdate();
    }
}
