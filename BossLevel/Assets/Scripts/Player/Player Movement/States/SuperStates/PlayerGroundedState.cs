using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int XInput;
    private bool _jumpInput;
    private bool _dashInput;
    
    public PlayerGroundedState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }
    

    public override void Enter()
    {
        base.Enter();
        PlayerController.PlayerRigidbody.gravityScale = PlayerData.baseGravityScale; // Reset gravity scale when entering grounded state
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        XInput = PlayerController.InputHandler.NormalizedXInput;
        _dashInput = PlayerController.InputHandler.DashInput;
        _jumpInput = PlayerController.InputHandler.JumpInput;

        if (_jumpInput && PlayerController.CheckIfGrounded())
        {
            PlayerController.InputHandler.ResetJumpInput();
            StateMachine.ChangeState(PlayerController.JumpState);
        }

        else if (_dashInput && PlayerController.DashState.CanDash())
        {
            PlayerController.InputHandler.ResetDashInput();
            StateMachine.ChangeState(PlayerController.DashState);
        }
    }
}
