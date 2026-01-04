using Darkmatter.Core;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Darkmatter.Domain
{
    public class EnemyStateMachine:StateMachine
    {
        public readonly IEnemyPawn enemyPawn;
        public readonly IEnemyAnimController enemyAnimController;
        public readonly EnemyConfigSO enemyConfig;
        public readonly IAudioService audioService;

        public EnemyStateMachine(IEnemyPawn pawn, IEnemyAnimController animController, IAudioService audioService, EnemyConfigSO enemyConfig)
        {
            enemyPawn = pawn;
            enemyAnimController = animController;
            this.audioService = audioService;
            this.enemyConfig = enemyConfig;
        }

        public void SetSpeed(float speed)
        {
            enemyPawn.EnemyAI.speed = speed;
        }

        public bool PlayerInChasingRange()
        {
            if(Vector3.Distance(enemyPawn.PlayerTarget.position,enemyPawn.ReturnMyPos()) < enemyConfig.visionRange)
            {
                return true;
            }
            return false;
        }

        public bool PlayerInAttackRange()
        {
            if(Vector3.Distance(enemyPawn.PlayerTarget.position,enemyPawn.ReturnMyPos())<enemyConfig.attackRange)
            {
                return true;
            }
            return false;
        }

        public void Die()
        {
            if (enemyPawn.isDead) return;
            enemyAnimController.PlayDeadAnim();
            audioService.PlaySFXAt(AudioId.Zombie_Death,enemyPawn.ReturnMyPos());
            enemyPawn.Die();

        }
    }
}
