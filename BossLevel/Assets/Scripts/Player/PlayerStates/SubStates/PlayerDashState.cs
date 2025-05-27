using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    private float _lastDashTime;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    
    public override void Enter()
    {
        base.Enter();
        Player.StartCoroutine(Dash());
        
    }

    public bool IsDashCooldownActive() => Time.time - _lastDashTime < PlayerData.dashCooldown;
    

    private IEnumerator Dash()
    {
        float originalGravityScale = Player.PlayerRigidbody.gravityScale;
        Player.PlayerRigidbody.gravityScale = 0f;
        Player.SetXVelocity(PlayerData.dashSpeed * Player.FacingDirection);
        Player.SetYVelocity(0f);
        yield return new WaitForSeconds(PlayerData.dashDuration);
        Player.PlayerRigidbody.gravityScale = originalGravityScale;
        Player.SetXVelocity(0f);
        _lastDashTime = Time.time;
        IsAbilityDone = true;
    }
    

    public bool CheckAbility() => IsAbilityDone;
    
}

//
// using UnityEngine;
//
// public class PlayerDashState : PlayerAbilityState
// {
//     private float _dashStartTime;
//     private float _originalGravityScale;
//     private float _originalXVelocity;
//     private bool _isDashing;
//     private float _lastDashTime;
//
//     public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName)
//         : base(player, stateMachine, playerData, animBoolName)
//     {
//     }
//
//     public override void Enter()
//     {
//         base.Enter();
//         _isDashing = true;
//         _dashStartTime = Time.time;
//         _originalGravityScale = Player.PlayerRigidbody.gravityScale;
//         _originalXVelocity = Player.CurrentVelocity.x;
//
//         Player.PlayerRigidbody.gravityScale = 0f;
//         Player.SetXVelocity(PlayerData.dashSpeed * Player.FacingDirection);
//         Player.SetYVelocity(0f);
//     }
//
//     public override void LogicUpdate()
//     {
//         if (_isDashing && Time.time - _dashStartTime < PlayerData.dashDuration) return;
//         {
//             EndDash();
//         }
//         base.LogicUpdate();
//     }
//
//     private void EndDash()
//     {
//         Player.PlayerRigidbody.gravityScale = _originalGravityScale;
//         Player.SetXVelocity(0);
//         _lastDashTime = Time.time;
//         IsAbilityDone = true;
//         _isDashing = false;
//     }
//
//     public bool IsDashCooldownActive() => Time.time - _lastDashTime < PlayerData.dashCooldown;
// }