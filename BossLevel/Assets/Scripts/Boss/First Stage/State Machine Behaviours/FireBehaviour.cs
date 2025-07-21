using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireBehaviour : StateMachineBehaviour
{
    #region Animation Hashes
    private static readonly int Idle = Animator.StringToHash("Idle");
    #endregion
    
    private int _zombiesToSpawn = 2;
    private ZombieSpawner _zombieSpawner;
    
    private void Awake()
    {
        HealthEventManager.FirstCrackEvent += UpdateZombiesToSpawn; // Subscribe to the event to update zombies to spawn
        HealthEventManager.SecondCrackEvent += UpdateZombiesToSpawn; // Subscribe to the event to update zombies to spawn
    }

    private void UpdateZombiesToSpawn()
    {
        _zombiesToSpawn *= 2; // Increment the number of zombies to spawn on each crack event
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombiesToSpawn = Random.Range(_zombiesToSpawn/2, _zombiesToSpawn);
        _zombieSpawner = animator.GetComponent<ZombieSpawner>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_zombiesToSpawn <= _zombieSpawner.SpawnCount)
        {
            animator.SetTrigger(Idle);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _zombieSpawner.ResetSpawnCount();
    }
}
