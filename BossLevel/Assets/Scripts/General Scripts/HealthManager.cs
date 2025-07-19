using System;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private DamageFlash damageFlash;
    
    public float CurrentHealth { get; private set; }

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Reduce current health by damage amount
        CurrentHealth -= damage;

        // Ensure current health does not drop below zero
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        // Trigger flash effect to indicate damage taken
        if (damageFlash != null)
        {
            damageFlash.CallDamageFlash();
        }

        // Check if health has reached zero
        if (CurrentHealth <= 0)
        {
            OnDeath();
        }
    }
    
    private void OnDeath()
    {
        gameObject.SetActive(false);
    }
}
