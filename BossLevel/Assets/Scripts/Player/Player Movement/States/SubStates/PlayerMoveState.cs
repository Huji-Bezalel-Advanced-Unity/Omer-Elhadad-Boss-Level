using System;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public static event Action PlayerMove; 

    public PlayerMoveState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        PlayerMove?.Invoke();
        base.Enter();
    }

    public override void LogicUpdate()
    {
        
        base.LogicUpdate();
        if (XInput != 0) return;
        StateMachine.ChangeState(PlayerController.IdleState);
        PlayerController.SetXVelocity(0f);
    }

    public override void PhysicsUpdate()
    {
        PlayerController.SetXVelocity(PlayerData.movementSpeed * XInput);
        PlayerController.CheckIfShouldFlip(XInput);
        base.PhysicsUpdate();
    }

}
