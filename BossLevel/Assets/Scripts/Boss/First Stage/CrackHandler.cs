// File: Assets/Scripts/Boss/First Stage/CrackHandler.cs
using System;
using UnityEngine;

public class CrackHandler : MonoBehaviour
{
    private static readonly int FirstCrack = Animator.StringToHash("FirstCrack");
    private static readonly int SecondCrack = Animator.StringToHash("SecondCrack");
    private static readonly int Death = Animator.StringToHash("Death");
    [SerializeField] private HealthManager bossHealthManager;
    [SerializeField] private Animator crackAnimator;
    [SerializeField] private Animator bossAnimator;
    

    public static event Action FirstCrackEvent;
    public static event Action SecondCrackEvent;

    private bool _firstCrackTriggered;
    private bool _secondCrackTriggered;
    private bool _bossDead;

    private void Update()
    {
        if (!_firstCrackTriggered && bossHealthManager.CurrentHealth <= 75f && bossHealthManager.CurrentHealth > 50f)
        {
            FirstCrackEvent?.Invoke();
            crackAnimator.SetTrigger(FirstCrack);
            _firstCrackTriggered = true;
        }
        else if (!_secondCrackTriggered && bossHealthManager.CurrentHealth <= 50f && bossHealthManager.CurrentHealth > 25f)
        {
            SecondCrackEvent?.Invoke();
            crackAnimator.SetTrigger(SecondCrack);
            _secondCrackTriggered = true;
        }
        else if (bossHealthManager.CurrentHealth <= 0f && !_bossDead)
        {
            _bossDead = true;
            //turn off the animator
            //play the death animation
            bossAnimator.Play(Death);
            gameObject.SetActive(false);
        }
    }
}