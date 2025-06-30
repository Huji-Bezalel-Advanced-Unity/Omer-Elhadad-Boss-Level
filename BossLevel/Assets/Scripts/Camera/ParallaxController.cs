// using System;
// using UnityEngine;
//
// public class ParallaxController : MonoBehaviour
// {
//     private Transform _cameraTransform;
//     private Vector3 _cameraStartPosition;
//     private float _distance;
//     private float _furthestBack;
//     
//     private GameObject[] _backgrounds;
//     private Material[] _materials;
//     private float[] _backSpeed;
//     
//     [Range(0.01f, 0.05f)] public float speed = 0.2f;
//     
//     void Start()
//     {
//         if (Camera.main != null) _cameraTransform = Camera.main.transform;
//         _cameraStartPosition = _cameraTransform.position;
//         
//         var backgroundCount = transform.childCount;
//         
//         _materials = new Material[backgroundCount];
//         _backSpeed = new float[backgroundCount];
//         
//         _backgrounds = new GameObject[backgroundCount];
//         
//         for (var i = 0; i < backgroundCount; i++)
//         {
//             _backgrounds[i] = transform.GetChild(i).gameObject;
//             _materials[i] = _backgrounds[i].GetComponent<Renderer>().material;
//         }
//         BackSpeedCalculation(backgroundCount);
//     }
//     
//     private void BackSpeedCalculation(int backCount)
//     {
//         foreach (var t in _backgrounds)
//         {
//             if (t.transform.position.z - _cameraTransform.position.z > _furthestBack)
//             {
//                 _furthestBack = t.transform.position.z - _cameraTransform.position.z;
//             }
//         }
//         
//         for (var i = 0; i < backCount; i++)
//         {
//             _backSpeed[i] = 1-(_backgrounds[i].transform.position.z - _cameraTransform.position.z) / _furthestBack;
//         }
//     }
//
//     private void LateUpdate()
//     {
//         _distance = _cameraTransform.position.x - _cameraStartPosition.x;
//         transform.position = new Vector3(_cameraTransform.position.x, transform.position.y);
//         
//         for (var i = 0; i < _backgrounds.Length; i++)
//         {
//             float speedFactor = _backSpeed[i] * speed;
//             _materials[i].SetTextureOffset("_MainTex", new Vector2(_distance, 0) * speedFactor);
//         }
//     }
// }

using System;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Assign in inspector

    private Vector3 _playerStartPosition;
    private float _distance;
    private float _furthestBack;

    private GameObject[] _backgrounds;
    private Material[] _materials;
    private float[] _backSpeed;

    [Range(0.01f, 0.05f)] public float speed = 0.2f;

    void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform not assigned to ParallaxController.");
            enabled = false;
            return;
        }
        _playerStartPosition = playerTransform.position;

        var backgroundCount = transform.childCount;

        _materials = new Material[backgroundCount];
        _backSpeed = new float[backgroundCount];

        _backgrounds = new GameObject[backgroundCount];

        for (var i = 0; i < backgroundCount; i++)
        {
            _backgrounds[i] = transform.GetChild(i).gameObject;
            _materials[i] = _backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculation(backgroundCount);
    }

    private void BackSpeedCalculation(int backCount)
    {
        foreach (var t in _backgrounds)
        {
            if (t.transform.position.z - playerTransform.position.z > _furthestBack)
            {
                _furthestBack = t.transform.position.z - playerTransform.position.z;
            }
        }

        for (var i = 0; i < backCount; i++)
        {
            _backSpeed[i] = 1 - (_backgrounds[i].transform.position.z - playerTransform.position.z) / _furthestBack;
        }
    }

    private void LateUpdate()
    {
        _distance = playerTransform.position.x - _playerStartPosition.x;

        // Remove this line to prevent snapping:
        // transform.position = new Vector3(playerTransform.position.x, transform.position.y);

        for (var i = 0; i < _backgrounds.Length; i++)
        {
            float speedFactor = _backSpeed[i] * speed;
            _materials[i].SetTextureOffset("_MainTex", new Vector2(_distance, 0) * speedFactor);
        }
    }
}
