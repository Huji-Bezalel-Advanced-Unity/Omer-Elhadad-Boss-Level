using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerController PlayerController;
    protected PlayerStateMachine StateMachine;
    protected PlayerData PlayerData;    
    protected bool IsAnimationFinished;
    
    protected float StartTime;
    
    private string _animBoolName;
    
    public PlayerState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
    {
        PlayerController = playerController;
        StateMachine = stateMachine;
        PlayerData = playerData;
        _animBoolName = animBoolName;
    }
    
    public virtual void Enter()
    {
        DoChecks();
        
        PlayerController.PlayerAnimator.SetBool(_animBoolName, true);
        StartTime = Time.deltaTime;
        IsAnimationFinished = false;
        // Debug.Log($"Entering state: {this.GetType().Name}");
    }
    
    public virtual void Exit()
    {
        PlayerController.PlayerAnimator.SetBool(_animBoolName, false);
    }
    
    public virtual void LogicUpdate()
    {
        DoChecks();
        // Logic to update the state
    }
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
        // Physics-related updates for the state
    }
    
    public virtual void DoChecks()
    {
        // Perform checks relevant to the state
    }
    
    public virtual void AnimationTrigger()
    {
        // Handle animation triggers if needed
    }

    public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
}
