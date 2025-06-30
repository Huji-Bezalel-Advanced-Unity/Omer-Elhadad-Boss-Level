using UnityEngine;

public class StageFollower : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField, Range(0f, 1f)] private float parallaxFactor = 0.5f;

    private Vector3 _startPosition;
    private Vector3 _playerStartPosition;

    void Start()
    {
        _startPosition = transform.position;
        _playerStartPosition = playerTransform.position;
    }

    void LateUpdate()
    {
        float playerDeltaX = playerTransform.position.x - _playerStartPosition.x;
        transform.position = new Vector3(
            _startPosition.x - playerDeltaX * parallaxFactor,
            _startPosition.y,
            _startPosition.z
        );
    }
}