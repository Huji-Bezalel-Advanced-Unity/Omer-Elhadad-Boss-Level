using System;
using UnityEngine;

public class PlayerHealthEvents : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    
    public static event Action PlayerDeathEvent;

    private void Update()
    {
        if (healthManager.CurrentHealth <= 0f)
        {
            Debug.Log("Player death");
            PlayerDeathEvent?.Invoke();
        }
    }
}
