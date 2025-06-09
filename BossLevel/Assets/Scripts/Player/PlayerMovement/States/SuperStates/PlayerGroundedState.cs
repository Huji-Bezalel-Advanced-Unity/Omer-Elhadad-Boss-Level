using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    protected bool IsGrounded;
    private bool _jumpInput;
    private bool _dashInput;
    
    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        Player.JumpState.ResetDoubleJump();
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        
        XInput = Player.InputHandler.NormalizedXInput;
        _dashInput = Player.InputHandler.DashInput;
        _jumpInput = Player.InputHandler.JumpInput;

        if (_jumpInput && IsGrounded)
        {
            Player.InputHandler.ResetJumpInput();
            StateMachine.ChangeState(Player.JumpState);
        }
        
        else if (_dashInput && !Player.DashState.IsDashCooldownActive())
        {
            Player.InputHandler.ResetDashInput();
            StateMachine.ChangeState(Player.DashState);
        }
        
        else if (!IsGrounded)
        {
            StateMachine.ChangeState(Player.InAirState);
        }
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    
    public override void DoChecks()
    {
        base.DoChecks();
        // Add any grounded checks here if needed
        IsGrounded = Player.CheckIfGrounded();
    }
    
}
