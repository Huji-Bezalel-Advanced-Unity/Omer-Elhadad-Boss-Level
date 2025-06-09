using System;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public static event Action PlayerIdle; 
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
            StateMachine.ChangeState(Player.MoveState);
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
