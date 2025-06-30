using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public Vector2 RawMovementInput { get; private set; }
    public int NormalizedXInput { get; private set; }
    public int NormalizedYInput { get; private set; }
     
    public bool JumpInput { get; private set; }
    
    public bool SwapInput { get; private set; }
    
    public bool ShootInput { get; private set; }
    
    private float _jumpInputStartTime;
    private float _dashInputStartTime;

    private const float InputHoldTime = 0.01f; // Time to hold the jump input to register as a jump
    
    public bool DashInput { get; private set; }

    private void Update()
    {
        CheckJumpInput();
        CheckDashInput();
    }

    public void OnMovementInput(InputAction.CallbackContext context)
    {
        RawMovementInput = context.ReadValue<Vector2>();
        NormalizedXInput = (int)(RawMovementInput*Vector2.right).normalized.x;
        NormalizedYInput = (int)(RawMovementInput*Vector2.up).normalized.y;
        
    }
    
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        JumpInput = true;
        _jumpInputStartTime = Time.time;
    }
    
    public void OnDashInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        DashInput = true;
        _dashInputStartTime = Time.time;
    }

    public void OnSwapInput(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        SwapInput = true;
    }
    
    public void OnShootInput(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            ShootInput = true;
        }
        else if (context.canceled)
        {
            ShootInput = false;
        }
    }
    
    private void CheckJumpInput()
    {
        if (Time.time - _jumpInputStartTime >= InputHoldTime)
        {
            JumpInput = false;
        }
    }

    private void CheckDashInput()
    {
        if (Time.time - _dashInputStartTime >= InputHoldTime)
        {
            DashInput = false;
        }
    }
    
    public void ResetDashInput() => DashInput = false;
    public void ResetJumpInput() => JumpInput = false;
    public void ResetSwapInput() => SwapInput = false;

    
    
}
