using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (XInput != 0)
        {
            Player.StateMachine.ChangeState(Player.MoveState);
        }
        else
        {
            Player.StateMachine.ChangeState(Player.IdleState);
        }
    }
}
