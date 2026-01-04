using Darkmatter.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Darkmatter.Domain
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly List<Transform> patrolPoints;
        private readonly Transform playerTransform;
        private readonly GameObject fatZombiePrefab;
        private readonly GameObject slimZombiePrefab;
        private readonly IObjectResolver objectResolver;

        public EnemyFactory(Transform playerTransform, List<Transform> patrolPoints, GameObject fatZombiePrefab, GameObject slimZombiePrefab,IObjectResolver resolver)
        {
            this.playerTransform = playerTransform;
            this.patrolPoints = patrolPoints;
            this.fatZombiePrefab = fatZombiePrefab;
            this.slimZombiePrefab = slimZombiePrefab;
            this.objectResolver = resolver;
        }
        public IEnemyPawn GetEnemy(ZombieType type)
        {
            GameObject enemyObj = null;

            switch (type)
            {
                case ZombieType.Fat:
                    enemyObj = GameObject.Instantiate(fatZombiePrefab, GetSpawnPos(), Quaternion.identity);
                    break;

                case ZombieType.slim:
                    enemyObj = GameObject.Instantiate(slimZombiePrefab, GetSpawnPos(), Quaternion.identity);
                    break;

                default:
                    break;
            }
            objectResolver.InjectGameObject(enemyObj);

            IEnemyPawn enemyPawn = enemyObj.GetComponent<IEnemyPawn>();
            enemyPawn.InitializeFromFactory(playerTransform, GetRandomPatrolPoints(Random.Range(4, patrolPoints.Count)));
            return enemyPawn;
        }

        private Vector3 GetSpawnPos()
        {
            return patrolPoints[Random.Range(0, patrolPoints.Count)].position;
        }

        private List<Transform> GetRandomPatrolPoints(int count)
        {
            return patrolPoints.OrderBy(x=>Random.value).Take(count).ToList();
            
        }
    }
}
