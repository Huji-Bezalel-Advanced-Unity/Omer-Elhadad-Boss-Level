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

    #region Attack Container Settings
    [SerializeField] private PlayerController playerController;
    
    [SerializeField] private RuntimeAnimatorController smallAttackAnimatorController;
    [SerializeField] private Transform[] smallAttackSpawnPoints;
    
    [SerializeField] private RuntimeAnimatorController largeAttackAnimatorController;
    [SerializeField] private Transform[] largeAttackSpawnPoints;
    
    [SerializeField] private AttackData attackData;
    #endregion
    
    public Animator AttackAnimator { get; private set; }
    public PlayerInputHandler InputHandler { get; private set; }
    
    private void Awake()
    {
        AttackAnimator = GetComponent<Animator>();
        StateMachine = new AttackStateMachine();
        SmallAttackState = new SmallAttackState(this, StateMachine, attackData, "transform", smallAttackAnimatorController, smallAttackSpawnPoints);
        LargeAttackState = new LargeAttackState(this, StateMachine, attackData, "transform", largeAttackAnimatorController, largeAttackSpawnPoints);
    }

    private void OnDisable()
    {
        SmallAttackState.Exit();
        LargeAttackState.Exit();
    }

    private void Start()
    {
        StateMachine.Initialize(SmallAttackState);
        InputHandler = playerController.InputHandler;
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
