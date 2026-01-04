using UnityEngine;

namespace Darkmatter.Core
{
    [CreateAssetMenu(fileName = "EnemyConfigSO", menuName = "Scriptable Objects/EnemyConfigSO")]
    public class EnemyConfigSO : ScriptableObject
    {
        [Header("Enemy Data")]
        public float walkSpeed = 3f;
        public float chaseSpeed = 5f;
        public float visionRange = 15f;
        public float attackRange = 2f;
    }
}
