using System.Collections;
using UnityEngine;

public class CanonBallSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnInterval = 0.2f;
    [SerializeField] private float maxSpawnInterval = 1.0f;

    private Coroutine _spawningCoroutine;

    private void OnEnable()
    {
        ThrowBehaviour.ThrowEvent += OnThrowEvent;
    }

    private void OnDisable()
    {
        ThrowBehaviour.ThrowEvent -= OnThrowEvent;
    }

    private void OnThrowEvent(float duration)
    {
        if (_spawningCoroutine != null)
            StopCoroutine(_spawningCoroutine);
        _spawningCoroutine = StartCoroutine(SpawnBallsRoutine(duration));
    }

    private IEnumerator SpawnBallsRoutine(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            var poolableCanonBall = CanonBallPool.Instance.Get();
            Vector3 spawnPosition = transform.position;
            spawnPosition.z = 4.5f; // Set desired z position
            poolableCanonBall.transform.position = spawnPosition;
            poolableCanonBall.transform.rotation = transform.rotation;
            poolableCanonBall.gameObject.SetActive(true);

            float waitTime = Random.Range(minSpawnInterval, maxSpawnInterval);
            yield return new WaitForSeconds(waitTime);
            timer += waitTime;
        }
        _spawningCoroutine = null;
    }
    
    //draw gizmos for the spawn point
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}