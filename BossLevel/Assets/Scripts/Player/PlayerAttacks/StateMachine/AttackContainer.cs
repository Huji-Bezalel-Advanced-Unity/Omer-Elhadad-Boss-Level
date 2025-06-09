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

    [SerializeField] private Player player;
    
    public Animator AttackAnimator { get; private set; }
    
    [SerializeField] private RuntimeAnimatorController smallAttackAnimatorController;
    [SerializeField] private RuntimeAnimatorController largeAttackAnimatorController;

    //public PlayerInputHandler InputHandler { get; private set; }
    
    [SerializeField] private AttackData attackData;
    
    
    
    private void Awake()
    {
        StateMachine = new AttackStateMachine();
        // Initialize states here, similar to PlayerStateMachine
        SmallAttackState = new SmallAttackState(this, StateMachine, attackData, "transform");
        LargeAttackState = new LargeAttackState(this, StateMachine, attackData, "transform");
    }
    
    private void Start()
    {
        AttackAnimator = GetComponent<Animator>();
        AttackAnimator.runtimeAnimatorController = smallAttackAnimatorController;
        StateMachine.Initialize(SmallAttackState);
    }

    private void Update()
    {
        StateMachine.CurrentState.LogicUpdate();
        if (!player.InputHandler.SwapInput) return;
        // Swap to the next attack state
            
        player.InputHandler.ResetSwapInput();
        if (StateMachine.CurrentState is SmallAttackState)
        {
            AttackAnimator.runtimeAnimatorController = largeAttackAnimatorController;
            StateMachine.ChangeState(LargeAttackState);
        }
        else if (StateMachine.CurrentState is LargeAttackState)
        {
            AttackAnimator.runtimeAnimatorController = smallAttackAnimatorController;
            StateMachine.ChangeState(SmallAttackState);
        }
    }
    
    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    
    private void AnimationTrigger() => StateMachine.CurrentState.AnimationTrigger();
    private void AnimationFinishTrigger() => StateMachine.CurrentState.AnimationFinishTrigger();
}
