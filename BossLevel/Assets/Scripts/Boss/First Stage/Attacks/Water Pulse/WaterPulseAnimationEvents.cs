using System;
using UnityEngine;

public class WaterPulseAnimationEvents : MonoBehaviour
{
    [Header("Water Pulse Settings")]
    [SerializeField] private int waterPulseSide;

    private void Awake()
    {
        WaterPulseBehaviour.WaterPulseActivated += OnWaterPulseActivated;
        gameObject.SetActive(false);
    }
    
    private void OnDestroy()
    {
        WaterPulseBehaviour.WaterPulseActivated -= OnWaterPulseActivated;
    }

    public void EndAnimationEvent()
    {
        gameObject.SetActive(false);
    }
    
    private void OnWaterPulseActivated(WaterPulseBehaviour.WaterPulseSide side)
    {
        var activatedSide = (int)side;
        if (waterPulseSide == activatedSide)
        {
            gameObject.SetActive(true);
        }
    }
}
