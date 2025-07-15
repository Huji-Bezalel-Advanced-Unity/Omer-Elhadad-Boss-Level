using UnityEngine;

public class IntroBehaviour : StateMachineBehaviour
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Move = Animator.StringToHash("Move");
    
    private int _rand;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _rand = Random.Range(0, 2);
        animator.SetTrigger(_rand == 0 ? Idle : Move);
    }
}
