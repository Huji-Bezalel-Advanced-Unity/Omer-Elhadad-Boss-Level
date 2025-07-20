using System;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float _originalGravityScale;

    // add event that player is dashing
    
    public static event Action PlayerDash;
    
    private float _lastDashTime;

    public PlayerDashState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(playerController, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        // Trigger the dash event
        PlayerDash?.Invoke();
        PlayerController.InputHandler.ResetDashInput();
        _originalGravityScale = PlayerController.PlayerRigidbody.gravityScale;
        PlayerController.PlayerRigidbody.gravityScale = 0f; // Disable gravity during dash
        base.Enter();
    }
    
    public override void PhysicsUpdate()
    {
        var dashVelocity = new Vector2(PlayerData.dashSpeed * PlayerController.FacingDirection, 0f);
        PlayerController.PlayerRigidbody.linearVelocity = dashVelocity;

        if (!IsAnimationFinished) return;
            
        var currentVel = PlayerController.PlayerRigidbody.linearVelocity;
        PlayerController.PlayerRigidbody.linearVelocity = new Vector2(0f, currentVel.y);
        IsAbilityDone = true;
        
    }
    
    public override void Exit()
    {
        base.Exit();
        PlayerController.PlayerRigidbody.gravityScale = _originalGravityScale; // Reset gravity scale after dash
        _lastDashTime = Time.time;
    }

    public bool CanDash() => _lastDashTime + PlayerData.dashCooldown <= Time.time;
}