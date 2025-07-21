using System;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplayer : MonoBehaviour
{
    #region Health Display Settings
    [SerializeField] private List<GameObject> heartObjects;
    [SerializeField] private HealthManager playerHealthManager;
    #endregion

    private int _lastHealth = -1;

    void Update()
    {
        int currentHealth = (int)playerHealthManager.CurrentHealth;
        if (currentHealth != _lastHealth)
        {
            for (int i = 0; i < heartObjects.Count; i++)
            {
                heartObjects[i].SetActive(i < currentHealth);
            }
            _lastHealth = currentHealth;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < heartObjects.Count; i++)
        {
            heartObjects[i].SetActive(i < 3);
        }
    }
}