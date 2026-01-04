using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class AttackState : State<EnemyStateMachine>
    {
        public AttackState(EnemyStateMachine runner) : base(runner) { }
        private IEnemyAnimController enemyAnimController => runner.enemyAnimController;
        private IEnemyPawn enemyPawn => runner.enemyPawn;

        public override void Enter()
        {
            base.Enter();
            enemyAnimController.PlayAttackAnim(true);
            enemyPawn.OnHealthDecreased += HandleHealth;
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
            HandleAttack();
            CheckForStateBreak();

        }

        private void CheckForStateBreak()
        {
            if(!runner.PlayerInAttackRange())
            {
                runner.ChangeState(new ChaseState(runner));
            }
        }

        private void HandleAttack()
        {
            Vector3 dir = (enemyPawn.PlayerTarget.position - enemyPawn.ReturnMyPos()).normalized;
            enemyPawn.GameObject.transform.rotation = Quaternion.LookRotation(dir);
            //rotate towards player and handle Attack here
        }

        public override void Exit()
        {
            base.Exit();
            enemyAnimController.PlayAttackAnim(false);
            enemyPawn.OnHealthDecreased -= HandleHealth;
        }
    }
}
