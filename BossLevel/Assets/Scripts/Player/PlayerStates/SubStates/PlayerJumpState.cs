using UnityEngine;

public class PlayerJumpState : PlayerAbilityState
{
    public bool CanDoubleJump { get; private set; } = true;
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Player.SetYVelocity(PlayerData.jumpSpeed);
        IsAbilityDone = true; 
    }

    public void PreformDoubleJump() => CanDoubleJump = false;
    public void ResetDoubleJump() => CanDoubleJump = true;
}
