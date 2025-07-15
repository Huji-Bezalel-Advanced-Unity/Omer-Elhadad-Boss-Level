using System;
using UnityEngine;

public class CrackHandler : MonoBehaviour
{
    [SerializeField] private HealthManager bossHealthManager;
    [SerializeField] private Animator crackAnimator;
    
    public static event Action FirstCrackEvent;
    public static event Action SecondCrackEvent;
    public static event Action ThirdCrackEvent;

    private void Update()
    {
        if (bossHealthManager.CurrentHealth <= 75f && bossHealthManager.CurrentHealth > 50f)
        {
            FirstCrackEvent?.Invoke();
            crackAnimator.SetTrigger("FirstCrack");
        }
        else if (bossHealthManager.CurrentHealth <= 50f && bossHealthManager.CurrentHealth > 25f)
        {
            SecondCrackEvent?.Invoke();
            crackAnimator.SetTrigger("SecondCrack");
        }
        else if (bossHealthManager.CurrentHealth <= 25f && bossHealthManager.CurrentHealth > 0f)
        {
            ThirdCrackEvent?.Invoke();
            crackAnimator.SetTrigger("ThirdCrack");
        }
    }
}
