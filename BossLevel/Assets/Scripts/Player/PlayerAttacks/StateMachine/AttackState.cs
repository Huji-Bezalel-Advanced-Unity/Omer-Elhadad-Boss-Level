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
    protected bool CanPlayOtherAnims;
    protected bool ShootFlag;
    protected Transform[] SpawnPoints;

    private string _animBoolName;
    
    public AttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController, Transform[] spawnPoints)
    {
        StateMachine = stateMachine;
        _animBoolName = animBoolName;
        PlayerAttackData = attackData;
        Container = container;
        AnimatorController = stateAnimatorController;
        SpawnPoints = spawnPoints;
    }
    
    public virtual void Enter()
    {
        Container.AttackAnimator.runtimeAnimatorController = AnimatorController;
        // Trigger the attack animation

        DoChecks();
        
        CanPlayOtherAnims = false;
        Container.AttackAnimator.Play($"transform");
        
        PlayerMoveState.PlayerMove += OnPlayerMove;
        PlayerIdleState.PlayerIdle += OnPlayerIdle;
        PlayerJumpState.PlayerJump += OnPlayerJump;
        PlayerInAirState.PlayerInAir += OnPlayerInAir;
        PlayerDashState.PlayerDash += OnPlayerDash;
        
    }
    

    protected virtual void OnPlayerIdle()
    {
        if (CanPlayOtherAnims && !Container.InputHandler.ShootInput)
            Container.AttackAnimator.Play($"idle");
    }

    protected virtual void OnPlayerMove()
    {
        if (CanPlayOtherAnims && !Container.InputHandler.ShootInput)
            Container.AttackAnimator.Play($"idle");
        
    }

    protected virtual void OnPlayerJump()
    {
        if (CanPlayOtherAnims && !Container.InputHandler.ShootInput)
        {
            Debug.Log("Playing jump animation");
            Container.AttackAnimator.Play($"jump");
        }
    }

    protected virtual void OnPlayerInAir()
    {
        if (CanPlayOtherAnims && !Container.InputHandler.ShootInput)
            Container.AttackAnimator.Play($"fall");
    }

    protected virtual void OnPlayerDash()
    {
        if (CanPlayOtherAnims && !Container.InputHandler.ShootInput)
            Container.AttackAnimator.Play($"dash");
    }

    public virtual void AnimationFinishTrigger()
    {
        CanPlayOtherAnims = true;
        Container.AttackAnimator.Play($"idle");
    }

    public virtual void Exit()
    {
        CanPlayOtherAnims = false;
        PlayerMoveState.PlayerMove -= OnPlayerMove;
        PlayerIdleState.PlayerIdle -= OnPlayerIdle;
        PlayerJumpState.PlayerJump -= OnPlayerJump;
        PlayerInAirState.PlayerInAir -= OnPlayerInAir;
        PlayerDashState.PlayerDash -= OnPlayerDash;
    }

    protected void ActivateShooting()
    {
        var stateInfo = Container.AttackAnimator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("shoot"))
        {
            Container.AttackAnimator.Play($"shoot");
            ShootFlag = true;
        }
    }
    
    public virtual void LogicUpdate()
    {
        if (Container.InputHandler.ShootInput)
            ActivateShooting();
        else if (CanPlayOtherAnims && ShootFlag)
        {
            Container.AttackAnimator.Play("idle");
            ShootFlag = false;
        }
        
    }
    
    public virtual void PhysicsUpdate()
    {
        DoChecks();
        // Physics-related updates for the state
    }
    
    protected virtual void DoChecks()
    {
        // Perform checks relevant to the state
    }
    
    public virtual void AnimationTrigger()
    {
        // Handle animation triggers if needed
    }

}