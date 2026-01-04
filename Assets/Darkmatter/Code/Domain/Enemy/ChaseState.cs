using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class ChaseState : State<EnemyStateMachine>
    {
        public ChaseState(EnemyStateMachine runner) : base(runner) { }
        private EnemyConfigSO enemyConfig => runner.enemyConfig;
        private IEnemyAnimController enemyAnimController => runner.enemyAnimController;
        private IEnemyPawn enemyPawn => runner.enemyPawn;


        public override void Enter()
        {
            base.Enter();
            runner.SetSpeed(enemyConfig.chaseSpeed);
            enemyAnimController.PlayeChaseAnim(true);
            enemyPawn.OnHealthDecreased += HandleHealth;
            runner.audioService.PlaySFXAt(AudioId.Zombie_Growl, enemyPawn.ReturnMyPos());
        }

        private void HandleHealth(float health)
        {
            if (health <= 0)
            {
                runner.Die();
            }
        }
        public override void Update()
        {
            if (enemyPawn.isDead) return;
            base.Update();
            HandleChase();
            CheckForStateBreak();
        }

        private void CheckForStateBreak()
        {
            if (!runner.PlayerInChasingRange())
            {
                runner.ChangeState(new PatrolState(runner));
            }
            else if(runner.PlayerInAttackRange())
            {
                runner.ChangeState(new AttackState(runner));
            }
        }

        private void HandleChase()
        {
            enemyPawn.SetDestination(enemyPawn.PlayerTarget.position);
        }

        public override void Exit()
        {
            base.Exit();
            enemyAnimController.PlayeChaseAnim(false);
            enemyPawn.OnHealthDecreased -= HandleHealth;

        }
    }
}
