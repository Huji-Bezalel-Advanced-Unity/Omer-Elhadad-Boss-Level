using System;
using UnityEngine;

public class PlayerHealthEvents : MonoBehaviour
{
    #region Player Health Events Settings
    [SerializeField] private HealthManager healthManager;
    #endregion
    
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
