using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public static event Action PlayerMove; 

    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        PlayerMove?.Invoke();
        base.Enter();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        
        if (XInput == 0)
        {
            StateMachine.ChangeState(Player.IdleState);
        }
        Player.SetXVelocity(PlayerData.movementSpeed * XInput);
        Player.CheckIfShouldFlip(XInput);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }
}
