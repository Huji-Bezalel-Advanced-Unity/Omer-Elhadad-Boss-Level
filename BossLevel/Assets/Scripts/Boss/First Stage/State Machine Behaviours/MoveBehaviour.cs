using UnityEngine;

public class MoveBehaviour : StateMachineBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    
    [SerializeField] private float moveSpeed = 2f; // Speed at which the object moves towards the player
    private float _randomMoveTime;
    private FirstStageMovement _firstStageMovement;
    private Transform _playerTransform;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _randomMoveTime = Random.Range(0.5f, 3f);
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
        _randomMoveTime -= Time.deltaTime;
        if (_randomMoveTime <= 0f || Mathf.Abs(_playerTransform.position.x - animator.transform.position.x) < 0.1f)
        {
            // Randomly switch to idle state
            animator.SetTrigger(Idle);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    
}
