using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public static event Action PlayerIdle; 
    public PlayerIdleState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        PlayerIdle?.Invoke();
        base.Enter();
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (XInput != 0)
        {
            StateMachine.ChangeState(PlayerController.MoveState);
        }
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
