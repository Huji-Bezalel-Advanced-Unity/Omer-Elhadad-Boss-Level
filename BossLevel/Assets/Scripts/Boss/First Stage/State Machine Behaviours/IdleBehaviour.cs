using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class IdleBehaviour : StateMachineBehaviour
{
    private static readonly int Move = Animator.StringToHash("Move");
    private static readonly int Throw = Animator.StringToHash("Throw");
    private static readonly int Fire = Animator.StringToHash("Fire");
    private static readonly int Water = Animator.StringToHash("Water");

    private float _idleTimer = 1f;
    private BossStateAction _randomAction;
    private int _currentCrack = 2;

    private void Awake()
    {
        CrackHandler.FirstCrackEvent += UpdateCrack; // Subscribe to the event to update cracks
        CrackHandler.SecondCrackEvent += UpdateCrack; // Subscribe to the event to update cracks
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CrackHandler.FirstCrackEvent += UpdateCrack;
        
        _currentCrack = Mathf.Clamp(_currentCrack, 2, 4);
        _idleTimer = Random.Range(0.8f, 2f);
        var values = (BossStateAction[])System.Enum.GetValues(typeof(BossStateAction));
        Debug.Log("Current Crack: " + _currentCrack);
        _randomAction = values[Random.Range(0, _currentCrack)];
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _idleTimer -= Time.deltaTime;
        if (!(_idleTimer <= 0f)) return;

        switch (_randomAction)
        {
            case BossStateAction.Move:
                animator.SetTrigger(Move);
                break;
            case BossStateAction.Throw:
                animator.SetTrigger(Throw);
                break;
            case BossStateAction.Fire:
                animator.SetTrigger(Fire);
                break;
            case BossStateAction.Water:
                animator.SetTrigger(Water);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    
    private void UpdateCrack()
    {
        _currentCrack += 1;
    }
    private enum BossStateAction
    {
        Move,
        Throw,
        Fire,
        Water
    }
    
    
}