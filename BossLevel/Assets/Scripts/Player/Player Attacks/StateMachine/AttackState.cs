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
    private bool _isPlayerDead;

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
        DoChecks();
        CanPlayOtherAnims = false;
        _isPlayerDead = false;
        Container.AttackAnimator.Play("transform");

        PlayerMoveState.PlayerMove += OnPlayerMove;
        PlayerIdleState.PlayerIdle += OnPlayerIdle;
        PlayerJumpState.PlayerJump += OnPlayerJump;
        PlayerInAirState.PlayerInAir += OnPlayerInAir;
        PlayerDashState.PlayerDash += OnPlayerDash;
        PlayerHealthEvents.PlayerDeathEvent += OnPlayerDeath;
    }

    public virtual void Exit()
    {
        PlayerMoveState.PlayerMove -= OnPlayerMove;
        PlayerIdleState.PlayerIdle -= OnPlayerIdle;
        PlayerJumpState.PlayerJump -= OnPlayerJump;
        PlayerInAirState.PlayerInAir -= OnPlayerInAir;
        PlayerDashState.PlayerDash -= OnPlayerDash;
        PlayerHealthEvents.PlayerDeathEvent -= OnPlayerDeath;
        CanPlayOtherAnims = false;
    }

    protected virtual void OnPlayerDeath()
    {
        _isPlayerDead = true;
        CanPlayOtherAnims = false;
        
        Container.AttackAnimator.Play("Death");
    }

    protected virtual void OnPlayerIdle()
    {
        if (_isPlayerDead || !CanPlayOtherAnims || Container.InputHandler.ShootInput) return;
        Container.AttackAnimator.Play("idle");
    }

    protected virtual void OnPlayerMove()
    {
        if (_isPlayerDead || !CanPlayOtherAnims || Container.InputHandler.ShootInput) return;
        Container.AttackAnimator.Play("idle");
    }

    protected virtual void OnPlayerJump()
    {
        if (_isPlayerDead || !CanPlayOtherAnims || Container.InputHandler.ShootInput) return;
        Container.AttackAnimator.Play("jump");
    }

    protected virtual void OnPlayerInAir()
    {
        if (_isPlayerDead || !CanPlayOtherAnims || Container.InputHandler.ShootInput) return;
        Container.AttackAnimator.Play("fall");
    }

    protected virtual void OnPlayerDash()
    {
        if (_isPlayerDead || !CanPlayOtherAnims || Container.InputHandler.ShootInput) return;
        Container.AttackAnimator.Play("dash");
    }

    public virtual void AnimationFinishTrigger()
    {
        if (_isPlayerDead) return;
        CanPlayOtherAnims = true;
        Container.AttackAnimator.Play("idle");
    }

    protected void ActivateShooting()
    {
        if (_isPlayerDead) return;
        var stateInfo = Container.AttackAnimator.GetCurrentAnimatorStateInfo(0);
        if (!stateInfo.IsName("shoot"))
        {
            Container.AttackAnimator.Play("shoot");
            ShootFlag = true;
        }
    }

    public virtual void LogicUpdate()
    {
        if (_isPlayerDead) return;
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