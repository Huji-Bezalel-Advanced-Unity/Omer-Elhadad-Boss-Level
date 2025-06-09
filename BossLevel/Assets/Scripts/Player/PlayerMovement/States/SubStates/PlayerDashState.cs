using System;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float _originalGravityScale;
    private float _lastDashTime;
    // add event that player is dashing
    
    public static event Action PlayerDash;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
        : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        // Trigger the dash event
        PlayerDash?.Invoke();
        base.Enter();
        _originalGravityScale = Player.PlayerRigidbody.gravityScale;
        Player.PlayerRigidbody.gravityScale = 0f; // Disable gravity during dash
    }
    
    

    public override void LogicUpdate()
    {
    }
    
    public override void PhysicsUpdate()
    {
        // base.PhysicsUpdate();
        //
        // // Check if the dash duration has elapsed
        // if (IsAnimationFinished)
        // {
        //     EndDash();
        // }
            
        var dashVelocity = new Vector2(PlayerData.dashSpeed * Player.FacingDirection, 0f);
        Player.PlayerRigidbody.linearVelocity = dashVelocity;

        if (!IsAnimationFinished) return;
            
        var currentVel = Player.PlayerRigidbody.linearVelocity;
        Player.PlayerRigidbody.linearVelocity = new Vector2(0f, currentVel.y);
        _lastDashTime = Time.time;
        Player.PlayerRigidbody.gravityScale = _originalGravityScale; // Reset gravity scale after dash

        if (Player.CheckIfGrounded())
            StateMachine.ChangeState(Player.IdleState);
        else
            StateMachine.ChangeState(Player.InAirState);
    }

    public bool IsDashCooldownActive() => Time.time - _lastDashTime < PlayerData.dashCooldown;
}