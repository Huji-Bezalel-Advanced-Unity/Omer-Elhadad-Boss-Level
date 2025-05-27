using UnityEngine;

public class PlayerInAirState : PlayerState
{
    private bool _isGrounded;
    private int _xInput;
    private bool _dashInput;
    private bool _jumpInput;
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        _xInput = Player.InputHandler.NormalizedXInput;
        _dashInput = Player.InputHandler.DashInput;
        _jumpInput = Player.InputHandler.JumpInput;
        
        if (_dashInput && !Player.DashState.IsDashCooldownActive())
        {
            Player.InputHandler.ResetDashInput();
            StateMachine.ChangeState(Player.DashState);
        }
        else if (_jumpInput && !Player.CheckIfGrounded() && Player.JumpState.CanDoubleJump)
        {
            Player.JumpState.PreformDoubleJump();
            StateMachine.ChangeState(Player.JumpState);
        }
        
        else if (Player.CheckIfGrounded() && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.LandState);
        }

        else
        {
            Player.CheckIfShouldFlip(_xInput);
            Player.SetXVelocity(PlayerData.movementSpeed * _xInput);
        }
        
    }
    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }
}
