using Darkmatter.Core;
using UnityEngine;
using UnityEngine.Pool;
using VContainer;

namespace Darkmatter.Presentation
{
    public class EnemiesSpawnner : MonoBehaviour
    {
        [Inject] IEnemyFactory _enemyFactory;
        [Inject] IGameScreenController gameScreenController;
        public int baseEnemyCount =2;
        private ObjectPool<IEnemyPawn> _enemyPool;

        private int killedEnemies = 0;
        private int enemiesMultiplier = 1;


        private void OnEnable()
        {
            EnemyMotor.OnEnemyDead += ReturnEnemy;
        }

        private void OnDisable()
        {
            EnemyMotor.OnEnemyDead -= ReturnEnemy;
        }
        private void Awake()
        {
            _enemyPool = new ObjectPool<IEnemyPawn>(
                createFunc: () => _enemyFactory.GetEnemy(GetRandomType()),
                actionOnGet: enemy => enemy.GameObject.SetActive(true),
                actionOnRelease: enemy => enemy.GameObject.SetActive(false),
                actionOnDestroy: enemy => Destroy(enemy.GameObject),
                collectionCheck: true,
                defaultCapacity: 10,
                maxSize: 50
            );
        }

        private ZombieType GetRandomType()
        {
            return Random.value > 0.5f ? ZombieType.Fat : ZombieType.slim;
        }

        private void Start()
        {
            SpawnWave(enemiesMultiplier);
        }

        private void SpawnWave(int multiplier)
        {
            gameScreenController.UpdateTotalZombiesCount(baseEnemyCount*multiplier);
            gameScreenController.UpdateRemainingZombiesCount(baseEnemyCount*multiplier);
            for (int i = 0; i < baseEnemyCount*multiplier; i++)
            {
                IEnemyPawn enemy = _enemyPool.Get();
                enemy.GameObject.transform.position = enemy.PatrolPoints[Random.Range(0, enemy.PatrolPoints.Count)].position;
            }
         
        }

        public void ReturnEnemy(IEnemyPawn enemy)
        {
            enemy.Reset();
            _enemyPool.Release(enemy);
            killedEnemies++;
            gameScreenController.UpdateRemainingZombiesCount(baseEnemyCount*enemiesMultiplier - killedEnemies);
            if(killedEnemies == baseEnemyCount*enemiesMultiplier)
            {
                killedEnemies = 0;
                enemiesMultiplier++;
                SpawnWave(enemiesMultiplier);
            }
        }
    }
}
