using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerController : MonoBehaviour
{   
    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }
    
    public PlayerJumpState JumpState { get; private set; }
    
    public PlayerInAirState InAirState { get; private set; }
    
    public PlayerLandState LandState { get; private set; }
    
    public PlayerDashState DashState { get; private set; }
    
    public PlayerDeathState DeathState { get; private set; }
    
    #endregion

    #region Player Components

    [SerializeField] private PlayerData playerData;
    public Animator PlayerAnimator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    public Rigidbody2D PlayerRigidbody { get; private set; }

    #endregion

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
        
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();

        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData, "idle");
        MoveState = new PlayerMoveState(this, StateMachine, playerData, "move");
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "jump");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "fall");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
        DeathState = new PlayerDeathState(this, StateMachine, playerData, "death");
        
        // Initialize states
        PlayerHealthEvents.PlayerDeathEvent += () => StateMachine.ChangeState(DeathState);
    }
    
    private void Start()
    {
        StateMachine.Initialize(IdleState);
        PlayerRigidbody.gravityScale = playerData.baseGravityScale;
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    
    private void FixedUpdate()
    {
        CurrentVelocity = PlayerRigidbody.linearVelocity;
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion
    
    #region Set Functions
    public void SetXVelocity(float xVelocity)
    {
        _tempVelocityVector.Set(xVelocity, CurrentVelocity.y);
        PlayerRigidbody.linearVelocity = _tempVelocityVector;
        CurrentVelocity = PlayerRigidbody.linearVelocity;
        _tempVelocityVector = Vector2.zero; // Reset temp vector to avoid carrying over values
    }
    
    public void SetYVelocity(float yVelocity)
    {
        _tempVelocityVector.Set(CurrentVelocity.x, yVelocity);
        PlayerRigidbody.linearVelocity = _tempVelocityVector;
        CurrentVelocity = PlayerRigidbody.linearVelocity;
        _tempVelocityVector = Vector2.zero; // Reset temp vector to avoid carrying over values
    }
    
    #endregion

    #region Check Functions
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
    #endregion

    #region Debug Functions

    private void OnDrawGizmos()
    {
        if (groundCheck == null || playerData == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, playerData.groundCheckRadius);
    }

    #endregion
    
    private void Flip()
    {
        FacingDirection *= -1;
        var localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
