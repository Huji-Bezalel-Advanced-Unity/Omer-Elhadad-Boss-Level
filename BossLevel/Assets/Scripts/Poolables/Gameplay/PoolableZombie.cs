using System;
using Unity.VisualScripting;
using UnityEngine;

public class PoolableZombie : MonoBehaviour, IPoolable
{
    private Vector3 _originalPosition;

    private void Awake()
    {
        _originalPosition = transform.position;
    }

    public void OnDeathEvent()
    {
        Debug.Log("OnDeathEvent called - returning zombie to pool.");
        ZombiePool.Instance.Return(this);
    }

    public void Reset()
    {
        // Reset local scale and position to original
        transform.localScale = Vector3.one;
        transform.position = _originalPosition;
    }
}