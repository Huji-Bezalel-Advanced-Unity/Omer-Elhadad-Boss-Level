using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    private float idleTimer = 1f;
    private int randomeAction;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer = Random.Range(0.8f, 2f);
        randomeAction = Random.Range(0, 3);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idleTimer -= Time.deltaTime;
        if (idleTimer <= 0f)
        {
            // Randomly switch to move or attack state
            if (randomeAction is 0 or 1)
            {
                animator.SetTrigger("Move");
            }
            else if (randomeAction == 2)
            {
                animator.SetTrigger("Throw");
            }
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
    
}
