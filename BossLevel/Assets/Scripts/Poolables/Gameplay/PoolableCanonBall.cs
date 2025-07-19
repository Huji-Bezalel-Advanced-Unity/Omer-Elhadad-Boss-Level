using UnityEngine;
using UnityEngine.Rendering;

public class PoolableCanonBall : MonoBehaviour, IPoolable
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SortingGroup sortingGroup;
    
    [SerializeField] private float minPower = 0.5f;
    [SerializeField] private float maxPower = 1.5f;
    [SerializeField] private float minTorque = 0.5f;
    [SerializeField] private float maxTorque = 2.0f;
    [SerializeField] private float minAngle = -30f; // degrees, left of up
    [SerializeField] private float maxAngle = 30f;  // degrees, right of up

    private bool _hasShot = false;

    private void FixedUpdate()
    {
        if (!_hasShot)
        {
            float randomPower = Random.Range(minPower, maxPower);
            float randomAngle = Random.Range(minAngle, maxAngle);
            float radians = randomAngle * Mathf.Deg2Rad;
            Vector2 direction = new Vector2(Mathf.Sin(radians), Mathf.Cos(radians)).normalized;

            rb.AddForce(direction * randomPower, ForceMode2D.Impulse);

            float randomTorque = Random.Range(minTorque, maxTorque) * (Random.value > 0.5f ? 1 : -1);
            rb.AddTorque(randomTorque, ForceMode2D.Impulse);

            _hasShot = true;
        }

        if (rb.linearVelocity.y < 0f && sortingGroup.sortingOrder == -1)
        {
            sortingGroup.sortingOrder = 4;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag($"Destroyer")) return;
        CanonBallPool.Instance.Return(this);
    }

    public void Reset()
    {
        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;
        transform.rotation = Quaternion.identity;
        var pos = transform.position;
        pos.z = 4.5f;
        transform.position = pos;
        _hasShot = false;
        sortingGroup.sortingOrder = -1;
    }
}