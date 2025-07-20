using System;
using UnityEngine;

public class WaterPulseAnimationEvents : MonoBehaviour
{
    [SerializeField] private int waterPulseSide;

    private void Awake()
    {
        Debug.Log("Water Pulse Animation Events Subscribe Awake");
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
