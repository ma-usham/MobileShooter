using Codice.Client.Common;
using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class PatrolState : State<EnemyStateMachine>
    {
        public PatrolState(EnemyStateMachine runner) : base(runner) { }
        private EnemyConfigSO enemyConfig => runner.enemyConfig;
        private IEnemyAnimController enemyAnimController => runner.enemyAnimController;
        private IEnemyPawn enemyPawn => runner.enemyPawn;
        private int currentPatrolPointIndex = 0;


        public override void Enter()
        {
            base.Enter();
            Debug.Log("Entered Patrol State");
            enemyPawn.OnHealthDecreased += HandleHealth;
            runner.SetSpeed(enemyConfig.walkSpeed);
            enemyAnimController.PlayWalkAnim(true);
        }

        private void HandleHealth(float health)
        {
            if(health<=0)
            {
                runner.Die();
            }
        }

        public override void Update()
        {
            if (enemyPawn.isDead) return;
            base.Update();
            HandlePatrol();
            CheckForStateBreak();
        }

        private void CheckForStateBreak()
        {
            if(runner.PlayerInChasingRange())
            {
                runner.ChangeState(new ChaseState(runner));
            }
        }

        private void HandlePatrol()
        {
            if (enemyPawn.PatrolPoints.Count == 0) return;
            Transform target = enemyPawn.PatrolPoints[currentPatrolPointIndex];
            enemyPawn.SetDestination(target.position);

            if(Vector3.Distance(target.position,enemyPawn.ReturnMyPos()) < 0.5f) //close enought to targetPatrolPoint
            {
                currentPatrolPointIndex = (currentPatrolPointIndex+1)%enemyPawn.PatrolPoints.Count;
            }
        }

        public override void Exit()
        {
            base.Exit();
            enemyAnimController.PlayWalkAnim(false);
            enemyPawn.OnHealthDecreased -= HandleHealth;
        }



    }
}
