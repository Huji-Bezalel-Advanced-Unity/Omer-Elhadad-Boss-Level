using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState
{

    protected AttackContainer Container;
    protected AttackStateMachine StateMachine;
    protected AttackData PlayerAttackData;
    protected float StartTime;
    protected RuntimeAnimatorController AnimatorController;
    private bool _canPlayOtherAnims;

    private string _animBoolName;
    
    public AttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController)
    {
        StateMachine = stateMachine;
        _animBoolName = animBoolName;
        PlayerAttackData = attackData;
        Container = container;
        AnimatorController = stateAnimatorController;
    }
    
    public virtual void Enter()
    {
        Container.AttackAnimator.runtimeAnimatorController = AnimatorController;
        // Trigger the attack ani   mation

        DoChecks();
        
        _canPlayOtherAnims = false;
        Debug.Log("Entering AttackState");
        Container.AttackAnimator.Play($"transform");
        Debug.Log("Attack animation started");
        
        PlayerMoveState.PlayerMove += OnPlayerMove;
        PlayerIdleState.PlayerIdle += OnPlayerIdle;
        PlayerJumpState.PlayerJump += OnPlayerJump;
        PlayerInAirState.PlayerInAir += OnPlayerInAir;
        PlayerDashState.PlayerDash += OnPlayerDash;
        
    }


    public virtual void OnPlayerIdle()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"idle");
    }

    public virtual void OnPlayerMove()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"idle");
        
    }

    public virtual void OnPlayerJump()
    {
        if (_canPlayOtherAnims)
        {
            Debug.Log("Playing jump animation");
            Container.AttackAnimator.Play($"jump");
        }
    }

    public virtual void OnPlayerInAir()
    {
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"fall");
    }

    public virtual void OnPlayerDash()
    {
        Debug.Log("Playing dash animation");
        // Ensure that the dash animation is played only if allowed
        Debug.Log($"Can play other anims: {_canPlayOtherAnims}");
        if (_canPlayOtherAnims)
            Container.AttackAnimator.Play($"dash");
    }

    public virtual void AnimationFinishTrigger()
    {
        _canPlayOtherAnims = true;
        Container.AttackAnimator.Play($"idle");
    }

    public virtual void Exit()
    {
        _canPlayOtherAnims = false;
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