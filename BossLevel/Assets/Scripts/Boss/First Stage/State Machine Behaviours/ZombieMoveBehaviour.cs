using UnityEngine;

public class ZombieMoveBehaviour : StateMachineBehaviour
{
    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float deathDistance = 0.5f; // Distance threshold for death

    private FirstStageMovement _firstStageMovement;
    private Transform _playerTransform;
    private HealthManager _zombieHealthManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _firstStageMovement = animator.GetComponent<FirstStageMovement>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
        _zombieHealthManager = animator.GetComponent<HealthManager>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_zombieHealthManager.CurrentHealth <= 0f ||
            Vector2.Distance(animator.transform.position, _playerTransform.position) < deathDistance)
        {
            animator.SetTrigger(Death);
            return;
        }

        _firstStageMovement.FlipToPlayer(_playerTransform);
        Vector3 direction = (_playerTransform.position - animator.transform.position).normalized;
        animator.transform.position += direction * moveSpeed * Time.deltaTime;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}