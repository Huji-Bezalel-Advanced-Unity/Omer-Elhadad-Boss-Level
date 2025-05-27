using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
