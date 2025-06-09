using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Player : MonoBehaviour
{   
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    public PlayerJumpState JumpState { get; private set; }
    
    public PlayerInAirState InAirState { get; private set; }
    
    public PlayerLandState LandState { get; private set; }
    
    public PlayerDashState DashState { get; private set; }
    
    #endregion
    
    public Animator PlayerAnimator { get; private set; }
    
    [SerializeField] private AnimationClip idleAnimationClip; 
    [SerializeField] private AnimationClip moveAnimationClip;
    [SerializeField] private AnimationClip jumpAnimationClip;
    [SerializeField] private AnimationClip dashAnimationClip;

    public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField] private PlayerData playerData;
    
    public Rigidbody2D PlayerRigidbody { get; private set; }

    #region Check Transforms
    
    [SerializeField] private Transform groundCheck;

    #endregion

    #region Variables
    public Vector2 CurrentVelocity { get; private set; }
    public int FacingDirection { get; private set; } = 1; // 1 for right, -1 for left
    
    private Vector2 _tempVelocityVector;
    
    #endregion
    
    #region Unity Callbacks
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "fall");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
    }
    // private void Awake()
    // {
    //     StateMachine = new PlayerStateMachine();
    //     IdleState = new PlayerIdleState(this, StateMachine, playerData, idleAnimationClip);
    //     MoveState = new PlayerMoveState(this, StateMachine, playerData, moveAnimationClip);
    //     JumpState = new PlayerJumpState(this, StateMachine, playerData, jumpAnimationClip);
    //     DashState = new PlayerDashState(this, StateMachine, playerData, dashAnimationClip);
    // }
    
    private void Start()
    {
        PlayerAnimator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerRigidbody.gravityScale = playerData.baseGravityScale;
    }

    private void Update()
    {
        CurrentVelocity = PlayerRigidbody.linearVelocity;
        StateMachine.CurrentState.LogicUpdate();
    }
    
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    
    #region Set Functions
    public void SetXVelocity(float xVelocity)
    {
        _tempVelocityVector.Set(xVelocity, CurrentVelocity.y);
        PlayerRigidbody.linearVelocity = _tempVelocityVector;
        CurrentVelocity = PlayerRigidbody.linearVelocity;
    }
    
    public void SetYVelocity(float yVelocity)
    {
        _tempVelocityVector.Set(CurrentVelocity.x, yVelocity);
        PlayerRigidbody.linearVelocity = _tempVelocityVector;
        CurrentVelocity = PlayerRigidbody.linearVelocity;
    }
    
    #endregion
    
    public void CheckIfShouldFlip(int xInput)
    {
        if (xInput != 0 && xInput != FacingDirection)
        {
            Flip();
        }
    }
    
    public bool CheckIfGrounded()
    {
        // draw a gizmo for the ground check
        
        return Physics2D.OverlapCircle(groundCheck.position, playerData.groundCheckRadius, playerData.groundLayer);
    }
    
    private void OnDrawGizmos()
    {
        if (groundCheck == null || playerData == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }

    private void Flip()
    {
        FacingDirection *= -1;
        var localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    public static float Map(float value, float from1, float to1, float from2, float to2, bool clamp = false)
    {
        float mappedValue = from2 + (to2 - from2) * ((value - from1) / (to1 - from1));
        return clamp ? Mathf.Clamp(mappedValue, Mathf.Min(from2, to2), Mathf.Max(from2, to2)) : mappedValue;
    }
    
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
