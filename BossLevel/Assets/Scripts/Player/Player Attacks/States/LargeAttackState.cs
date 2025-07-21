using UnityEngine;

public class LargeAttackState : AttackState
{
    public LargeAttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController, Transform[] spawnPoints) : 
        base(container, stateMachine, attackData, animBoolName, stateAnimatorController, spawnPoints)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!Container.InputHandler.SwapInput || Container.InputHandler.ShootInput) return;
        Container.InputHandler.ResetSwapInput();
        StateMachine.ChangeState(Container.SmallAttackState);
    }
    
    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
        
        // Spawn a projectile at each spawn point
        if (SpawnPoints == null) return;
        foreach (var spawnPoint in SpawnPoints)
        {
            var projectile = LargeProjectilePool.Instance.Get();
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.gameObject.SetActive(true);
        }
    }
}