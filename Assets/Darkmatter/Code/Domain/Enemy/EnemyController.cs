using Darkmatter.Core;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Darkmatter.Domain
{
    public class EnemyController : MonoBehaviour
    {
        EnemyStateMachine esm;
        IEnemyAnimController animController;
        IEnemyPawn enemy;
        [SerializeField] public EnemyConfigSO enemyConfig;
        [Inject] IAudioService audioService;

        private void Awake()
        {
            animController = this.GetComponent<IEnemyAnimController>();
            enemy = this.GetComponent<IEnemyPawn>();
        }
        public void Start()
        {
            esm = new EnemyStateMachine(enemy,animController,audioService, enemyConfig);
            esm.ChangeState(new PatrolState(esm));
        }

        public void Update()
        {
            esm.Update();
        }
    }
}
