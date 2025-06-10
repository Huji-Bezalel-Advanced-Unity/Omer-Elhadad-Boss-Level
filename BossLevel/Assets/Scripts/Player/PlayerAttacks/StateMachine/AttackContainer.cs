using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;

public class AttackContainer : MonoBehaviour
{   
    #region State Variables
    public AttackStateMachine StateMachine { get; private set; }
    
    public SmallAttackState SmallAttackState { get; private set; }
    
    public LargeAttackState LargeAttackState { get; private set; }
    
    #endregion

    [SerializeField] private PlayerController playerController;
    
    public Animator AttackAnimator { get; private set; }
    
    [SerializeField] private RuntimeAnimatorController smallAttackAnimatorController;
    [SerializeField] private RuntimeAnimatorController largeAttackAnimatorController;

    public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField] private AttackData attackData;
    
    
    
    private void Awake()
    {
       
        AttackAnimator = GetComponent<Animator>();
        StateMachine = new AttackStateMachine();
        SmallAttackState = new SmallAttackState(this, StateMachine, attackData, "transform", smallAttackAnimatorController);
        LargeAttackState = new LargeAttackState(this, StateMachine, attackData, "transform", largeAttackAnimatorController);
        
        // Initialize states here, similar to PlayerStateMachine

    }
    
    private void Start()
    {
        StateMachine.Initialize(SmallAttackState);
        InputHandler = playerController.InputHandler;
        if (InputHandler == null )
        {
            Debug.LogError("InputHandler is not assigned in AttackContainer.");
        }
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
    }
    
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
