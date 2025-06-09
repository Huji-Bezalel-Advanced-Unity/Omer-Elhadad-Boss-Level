using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState
{

    protected AttackContainer Container;
    protected AttackStateMachine StateMachine;
    protected bool IsAnimationFinished;
    protected AttackData PlayerAttackData;
    protected float StartTime;
    
    private bool _canPlayOtherAnims = false;

    
    private string _animBoolName;
    
    public AttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName)
    {
        StateMachine = stateMachine;
        _animBoolName = animBoolName;
        PlayerAttackData = attackData;
        Container = container;
    }
    
    public virtual void Enter()
    {

        // Trigger the attack animation
        

        DoChecks();
        _canPlayOtherAnims = false;
        Debug.Log("Entering AttackState");
        Container.AttackAnimator.Play($"smallToLargeTransform");
        Debug.Log("Attack animation started");
        IsAnimationFinished = false;
        
        PlayerMoveState.PlayerMove += OnPlayerMove;
        PlayerIdleState.PlayerIdle += OnPlayerIdle;
        PlayerJumpState.PlayerJump += OnPlayerJump;
        PlayerInAirState.PlayerInAir += OnPlayerInAir;
        PlayerDashState.PlayerDash += OnPlayerDash;
        
    }


    protected virtual void OnPlayerIdle()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"idle");
    }

    protected virtual void OnPlayerMove()
    {
        if (_canPlayOtherAnims)
        {
            Container.AttackAnimator.Play($"move");
        }
    }

    protected virtual void OnPlayerJump()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"jump");
    }

    protected virtual void OnPlayerInAir()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"fall");
    }

    protected virtual void OnPlayerDash()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"dash");
    }

    public virtual void AnimationFinishTrigger()
    {
        IsAnimationFinished = true;
        _canPlayOtherAnims = true;
    }

    public virtual void Exit()
    {
        Container.AttackAnimator.SetBool(_animBoolName, false);
        PlayerMoveState.PlayerMove -= OnPlayerMove;
        PlayerIdleState.PlayerIdle -= OnPlayerIdle;
        PlayerJumpState.PlayerJump -= OnPlayerJump;
        PlayerInAirState.PlayerInAir -= OnPlayerInAir;
        PlayerDashState.PlayerDash -= OnPlayerDash;
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

}