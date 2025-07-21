using System;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private int _xInput;
    private bool _dashInput;
    
    public static event Action PlayerInAir; 

    public PlayerInAirState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void Enter()
    {
        PlayerInAir?.Invoke();
        
        base.Enter();
        PlayerController.PlayerRigidbody.gravityScale = PlayerData.inAirGravityScale; // Set gravity scale when entering in-air state
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        _xInput = PlayerController.InputHandler.NormalizedXInput;
        _dashInput = PlayerController.InputHandler.DashInput;
        
        if (_dashInput && PlayerController.DashState.CanDash())
        {
            PlayerController.InputHandler.ResetDashInput();
            StateMachine.ChangeState(PlayerController.DashState);
        }
        else if (PlayerController.CheckIfGrounded())
        {
            PlayerController.StateMachine.ChangeState(PlayerController.IdleState);
        }
    }
    
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
        PlayerController.SetXVelocity(PlayerData.movementSpeed * _xInput);
        PlayerController.CheckIfShouldFlip(_xInput);
    }
    
    public override void Exit()
    {
        base.Exit();
        PlayerController.PlayerRigidbody.gravityScale = PlayerData.baseGravityScale; // Reset gravity scale when exiting in-air state
    }
}
