// File: Assets/Scripts/Boss/First Stage/CrackHandler.cs
using System;
using UnityEngine;

public class HealthEventManager : MonoBehaviour
{
    #region Animator Hashes
    private static readonly int FirstCrack = Animator.StringToHash("FirstCrack");
    private static readonly int SecondCrack = Animator.StringToHash("SecondCrack");
    private static readonly int Death = Animator.StringToHash("Death");
    #endregion
    
    #region Animator Settings
    [SerializeField] private HealthManager bossHealthManager;
    [SerializeField] private Animator crackAnimator;
    [SerializeField] private Animator bossAnimator;
    #endregion

    #region Events
    public static event Action FirstCrackEvent;
    public static event Action SecondCrackEvent;
    public static event Action BossDeathEvent;
    #endregion
    
    private bool _firstCrackTriggered;
    private bool _secondCrackTriggered;
    private bool _bossDead;

    private void Update()
    {
        if (!_firstCrackTriggered && bossHealthManager.CurrentHealth <= 150f && bossHealthManager.CurrentHealth > 100f)
        {
            FirstCrackEvent?.Invoke();
            crackAnimator.SetTrigger(FirstCrack);
            _firstCrackTriggered = true;
        }
        else if (!_secondCrackTriggered && bossHealthManager.CurrentHealth <= 100f && bossHealthManager.CurrentHealth > 75f)
        {
            SecondCrackEvent?.Invoke();
            crackAnimator.SetTrigger(SecondCrack);
            _secondCrackTriggered = true;
        }
        else if (bossHealthManager.CurrentHealth <= 0f && !_bossDead)
        {
            _bossDead = true;
            bossAnimator.Play(Death);
            
            BossDeathEvent?.Invoke();
            gameObject.SetActive(false);
        }
    }
}