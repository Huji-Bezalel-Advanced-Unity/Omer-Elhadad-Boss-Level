// File: Assets/Scripts/Boss/First Stage/CrackHandler.cs
using System;
using UnityEngine;

public class CrackHandler : MonoBehaviour
{
    private static readonly int FirstCrack = Animator.StringToHash("FirstCrack");
    private static readonly int SecondCrack = Animator.StringToHash("SecondCrack");
    [SerializeField] private HealthManager bossHealthManager;
    [SerializeField] private Animator crackAnimator;

    public static event Action FirstCrackEvent;
    public static event Action SecondCrackEvent;

    private bool _firstCrackTriggered;
    private bool _secondCrackTriggered;

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
    }
}