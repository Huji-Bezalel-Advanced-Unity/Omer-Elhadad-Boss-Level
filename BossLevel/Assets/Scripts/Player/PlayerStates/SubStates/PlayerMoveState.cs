using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Exit()
    {
        base.Exit();
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
