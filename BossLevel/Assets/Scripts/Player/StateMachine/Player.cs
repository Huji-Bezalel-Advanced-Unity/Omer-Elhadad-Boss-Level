using System;
using UnityEditor;
using UnityEngine;

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
    
    public Animator Animator { get; private set; }

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
        JumpState = new PlayerJumpState(this, StateMachine, playerData, "inAir");
        InAirState = new PlayerInAirState(this, StateMachine, playerData, "inAir");
        LandState = new PlayerLandState(this, StateMachine, playerData, "land");
        DashState = new PlayerDashState(this, StateMachine, playerData, "dash");
    }
    
    private void Start()
    {
        Animator = GetComponent<Animator>();
        StateMachine.Initialize(IdleState);
        InputHandler = GetComponent<PlayerInputHandler>();
        PlayerRigidbody = GetComponent<Rigidbody2D>();
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
}
