using System;
using UnityEngine;

public class DeathBehaviour : StateMachineBehaviour
{
    private PoolableZombie _poolableZombie;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _poolableZombie = animator.GetComponent<PoolableZombie>();
    }
    

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _poolableZombie.OnDeathEvent();
    }
}
