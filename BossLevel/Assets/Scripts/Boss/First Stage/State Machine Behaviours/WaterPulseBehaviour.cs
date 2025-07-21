using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaterPulseBehaviour : StateMachineBehaviour
{
    #region Animation Hashes
    private static readonly int Idle = Animator.StringToHash("Idle");
    #endregion
    public enum WaterPulseSide
    {
        Left,
        Right
    }

    public static event Action<WaterPulseSide> WaterPulseActivated;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger(Idle);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.SetActive(true);
        var randomSide = (WaterPulseSide)Random.Range(0, Enum.GetValues(typeof(WaterPulseSide)).Length);
        WaterPulseActivated?.Invoke(randomSide);
    }
}