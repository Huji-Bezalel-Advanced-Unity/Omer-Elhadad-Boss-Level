using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireBehaviour : StateMachineBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    
    private int _zombiesToSpawn;
    private FirstStageSpawner _firstStageSpawner;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombiesToSpawn = Random.Range(1, 4);
        _firstStageSpawner = animator.GetComponent<FirstStageSpawner>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_zombiesToSpawn <= _firstStageSpawner.SpawnCount)
        {
            animator.SetTrigger(Idle);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _firstStageSpawner.ResetSpawnCount();
    }
}
