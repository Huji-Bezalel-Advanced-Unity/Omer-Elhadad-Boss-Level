using UnityEngine;

public class PlayerAbilityState : PlayerState
{
    protected int XInput;
    protected bool IsDashing;

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
        if (!Player.CheckIfGrounded() && Player.CurrentVelocity.y < 0.01f)
        {
            StateMachine.ChangeState(Player.InAirState);
        }
        else{
            StateMachine.ChangeState(Player.LandState);
        }
        
    }
    
    public override void Exit()
    {
        base.Exit();
        IsAbilityDone = true;
    }
    

    public override void DoChecks()
    {
        base.DoChecks();
        _isGrounded = Player.CheckIfGrounded();
    }
    
}
