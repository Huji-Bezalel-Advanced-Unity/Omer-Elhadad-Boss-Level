using System;
using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public bool CanDoubleJump { get; private set; } = true;
    
    public static event Action PlayerJump; 

    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        // Trigger the jump event
        PlayerJump?.Invoke();
        
        base.Enter();
        Player.SetYVelocity(PlayerData.jumpSpeed);
        IsAbilityDone = true;
    }
    
    public override void LogicUpdate()
    {
        if (!(Player.CurrentVelocity.y < 0)) return;
        base.LogicUpdate();
    }
    
    

    public void PreformDoubleJump() => CanDoubleJump = false;
    public void ResetDoubleJump() => CanDoubleJump = true;
}
