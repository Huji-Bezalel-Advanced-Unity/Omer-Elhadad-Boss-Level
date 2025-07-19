using UnityEngine;

public class PlayerHitboxLogic : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Enemy Attack"))
        {
            healthManager.TakeDamage(1f);
        }
    }
}
