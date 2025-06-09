using UnityEngine;

[CreateAssetMenu(fileName = "AttackData", menuName = "AttackData")]
public class AttackData : ScriptableObject
{
    [Header("Small Attack State")] public float smallAttackSpeed = 10f;
    public float smallAttackDuration = 0.5f; // Duration of the small attack

    [Header("Big Attack State")] public float bigAttackSpeed = 5f;
    public float bigAttackDuration = 1f; // Duration of the big attack
}
