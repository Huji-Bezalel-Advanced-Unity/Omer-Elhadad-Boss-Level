using System;
using UnityEngine;

public class EnemyHitboxLogic : MonoBehaviour
{
    [Header("Enemy Hitbox Settings")]
    [SerializeField] private HealthManager healthManager;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Large Projectile"))
        {
            healthManager.TakeDamage(other.GetComponent<PoolableLargeProjectile>().ProjectileDamage);
        }

        else if (other.CompareTag($"Small Projectile"))
        {
            healthManager.TakeDamage(other.GetComponent<PoolableSmallProjectile>().ProjectileDamage);
        }
    }
}
