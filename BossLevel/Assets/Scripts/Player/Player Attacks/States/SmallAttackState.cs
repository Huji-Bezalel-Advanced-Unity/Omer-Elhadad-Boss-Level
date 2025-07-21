using UnityEngine;

public class SmallAttackState : AttackState
{
    public SmallAttackState(AttackContainer container, AttackStateMachine stateMachine, AttackData attackData, string animBoolName, RuntimeAnimatorController stateAnimatorController, Transform[] spawnPoints) : 
        base(container, stateMachine, attackData, animBoolName, stateAnimatorController, spawnPoints)
    {
    }
    
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (!Container.InputHandler.SwapInput || Container.InputHandler.ShootInput) return;
        Container.InputHandler.ResetSwapInput();
        StateMachine.ChangeState(Container.LargeAttackState);
    }
    
    public override void AnimationTrigger()
    {
        // Spawn a projectile at each spawn point
        base.AnimationTrigger();
        
        if (SpawnPoints == null) return;
        foreach (var spawnPoint in SpawnPoints)
        {
            var projectile = SmallProjectilePool.Instance.Get();
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.gameObject.SetActive(true);
        }
    }
}
