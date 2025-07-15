using UnityEngine;

public class ZombieMoveBehaviour : StateMachineBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");
    
    [SerializeField] private float moveSpeed = 1f; // Speed at which the object moves towards the player
    private FirstStageMovement _firstStageMovement;
    private Transform _playerTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _firstStageMovement = animator.GetComponent<FirstStageMovement>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // look at player
        _firstStageMovement.FlipToPlayer(_playerTransform);
        Vector3 direction = (_playerTransform.position - animator.transform.position).normalized;
        animator.transform.position += direction * moveSpeed * Time.deltaTime;
        

        // Check if the random time has elapsed
        if (Mathf.Abs(_playerTransform.position.x - animator.transform.position.x) < 0.1f)
        {
            // Randomly switch to idle state
            animator.SetTrigger(Death);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
