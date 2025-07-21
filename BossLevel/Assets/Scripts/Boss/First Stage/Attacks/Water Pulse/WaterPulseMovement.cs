using System;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterPulseMovement : MonoBehaviour
{
    #region Water Pulse Movement Settings
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private float speed = 5f;
    #endregion
    
    private Rigidbody2D _rb;
    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb == null) return;
        Vector2 direction = (endPoint.position - startPoint.position).normalized;
        _rb.linearVelocity = direction * speed;
    }

    private void Update()
    {
        if (!(Vector2.Distance(gameObject.transform.position, endPoint.position) < 0.5f)) return;
        
        gameObject.transform.position = startPoint.position;
        _rb.position = startPoint.position; // Reset position
        _rb.linearVelocity = Vector2.zero; // Reset velocity
    }
    

    private void OnDisable()
    {
        if (_rb == null) return;
        
        gameObject.transform.position = startPoint.position;
        _rb.position = startPoint.position;
        _rb.linearVelocity = Vector2.zero;
    }
}
