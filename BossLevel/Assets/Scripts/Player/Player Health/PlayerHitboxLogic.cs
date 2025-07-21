using UnityEngine;

public class PlayerHitboxLogic : MonoBehaviour
{
    #region Health Manager Settings
    [SerializeField] private HealthManager healthManager;
    #endregion

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag($"Enemy Attack"))
        {
            healthManager.TakeDamage(1f);
        }
    }
}
