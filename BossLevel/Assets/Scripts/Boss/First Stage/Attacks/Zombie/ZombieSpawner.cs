using UnityEngine;
using Random = UnityEngine.Random;

public class ZombieSpawner : MonoBehaviour
{
    #region Zombie Spawner Settings
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float forceMagnitude = 5f;
    [SerializeField] private Transform playerTransform; // Assign in inspector
    #endregion

    public int SpawnCount { get; private set; } = 0;

    public void SpawnZombie()
    {
        var zombie = ZombiePool.Instance.Get();
        zombie.transform.position = spawnPoint.position;
        zombie.transform.rotation = spawnPoint.rotation;
        zombie.gameObject.SetActive(true);

        // Flip zombie to face player
        if (playerTransform != null)
        {
            Vector3 scale = zombie.transform.localScale;
            scale.x = playerTransform.position.x < zombie.transform.position.x ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            zombie.transform.localScale = scale;
        }

        // Add random force to the right or left (2D)
        var rb = zombie.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            int direction = Random.value < 0.5f ? -1 : 1;
            Vector2 force = new Vector2(direction * forceMagnitude, 0f);
            rb.AddForce(force, ForceMode2D.Impulse);
        }

        SpawnCount++;
    }

    public void ResetSpawnCount()
    {
        SpawnCount = 0;
    }
}