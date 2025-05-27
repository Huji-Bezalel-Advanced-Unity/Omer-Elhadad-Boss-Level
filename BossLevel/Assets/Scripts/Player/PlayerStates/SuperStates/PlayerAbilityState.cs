using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected int XInput;
    protected bool IsAbilityDone;
    private bool _isGrounded;
    public PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        IsAbilityDone = false;
        
    }
    

    // ReSharper disable Unity.PerformanceAnalysis
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!IsAbilityDone) return;
        XInput = Player.InputHandler.NormalizedXInput;
        Debug.Log(_isGrounded);
        if (!Player.CheckIfGrounded())
        {
            Debug.Log("PlayerAbilityState: LogicUpdate - Player is not grounded");
            StateMachine.ChangeState(Player.InAirState);
        }
        else if (XInput != 0 && Player.CurrentVelocity.x == 0)
        {
            Debug.Log("PlayerAbilityState: LogicUpdate - Player is grounded and moving");
            StateMachine.ChangeState(Player.MoveState);
        }
        else if (XInput == 0 && Player.CurrentVelocity.x == 0)
        {
            Debug.Log("PlayerAbilityState: LogicUpdate - Player is grounded and idle");
            StateMachine.ChangeState(Player.IdleState);
        }
        
    }
    

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }
    
}
