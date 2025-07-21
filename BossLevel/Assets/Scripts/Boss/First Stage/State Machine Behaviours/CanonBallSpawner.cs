using System.Collections;
using UnityEngine;

public class CanonBallSpawner : MonoBehaviour
{
    #region Canon Ball Spawner Settings
    [SerializeField] private float minSpawnInterval = 0.4f;
    [SerializeField] private float maxSpawnInterval = 1.0f;
    #endregion

    private Coroutine _spawningCoroutine;

    private void OnEnable()
    {
        ThrowBehaviour.ThrowEvent += OnThrowEvent;
        HealthEventManager.FirstCrackEvent += OnCrackEvent;
        HealthEventManager.SecondCrackEvent += OnCrackEvent;
    }

    private void OnDisable()
    {
        ThrowBehaviour.ThrowEvent -= OnThrowEvent;
    }
    
    private void OnCrackEvent()
    {
        maxSpawnInterval /= 2;
        minSpawnInterval /= 2;
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