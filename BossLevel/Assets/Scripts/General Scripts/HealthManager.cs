using UnityEngine;

public class HealthManager : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private DamageFlash damageFlash;
    [SerializeField] private AudioSource audioSource; // Single AudioSource
    [SerializeField] private AudioClip damageClip;    // Assign in inspector
    [SerializeField] private AudioClip deathClip;     // Assign in inspector

    private bool _isDead;

    public float CurrentHealth { get; private set; }

    private void OnEnable()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        if (CurrentHealth < 0) CurrentHealth = 0;

        if (damageFlash != null) damageFlash.CallDamageFlash();

        if (audioSource != null && damageClip != null)
        {
            audioSource.clip = damageClip;
            audioSource.Play();
        }

        if (CurrentHealth <= 0) OnDeath();
    }

    private void OnDeath()
    {
        if (audioSource == null || deathClip == null || _isDead) return;
        _isDead = true;
        audioSource.clip = deathClip;
        audioSource.Play();
    }
}