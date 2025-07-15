using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ThrowBehaviour : StateMachineBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    
    private float _throwTimer;
    public static event Action<float> ThrowEvent;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _throwTimer = Random.Range(2f, 4f);
        ThrowEvent?.Invoke(_throwTimer);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_throwTimer <= 0f)
            animator.SetTrigger(Idle);
        else
            _throwTimer -= Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Throw");
    }

}
