using UnityEngine;

public class WaterPulseBehaviour : StateMachineBehaviour
{
    [SerializeField] private GameObject[] leftAndRightWaterPulses;
    private GameObject _randomWaterPulse;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetTrigger("Idle");
    }
    

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var randomIndex = Random.Range(0, leftAndRightWaterPulses.Length);
        _randomWaterPulse = Instantiate(leftAndRightWaterPulses[randomIndex],
            leftAndRightWaterPulses[randomIndex].transform.position,
            leftAndRightWaterPulses[randomIndex].transform.rotation);       
        _randomWaterPulse.SetActive(true);
    }
}
