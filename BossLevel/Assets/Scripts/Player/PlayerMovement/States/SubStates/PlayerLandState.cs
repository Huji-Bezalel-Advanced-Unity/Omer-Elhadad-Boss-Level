using UnityEngine;

public class PlayerLandState : PlayerGroundedState
{
    public PlayerLandState(PlayerController playerController, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
    {
    }


    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (XInput != 0)
        {
            PlayerController.StateMachine.ChangeState(PlayerController.MoveState);
        }
        else
        {
            PlayerController.StateMachine.ChangeState(PlayerController.IdleState);
        }
    }
}
