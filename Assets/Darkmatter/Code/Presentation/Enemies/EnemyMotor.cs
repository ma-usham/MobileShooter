using Darkmatter.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using VContainer;

namespace Darkmatter.Presentation
{
    public class EnemyMotor : MonoBehaviour, IEnemyPawn
    {
        public static event Action<IEnemyPawn> OnEnemyDead;
        public GameObject GameObject => this.gameObject;
        [SerializeField] private NavMeshAgent enemyAI;

        public Transform PlayerTarget { get; private set; }
        public NavMeshAgent EnemyAI => enemyAI;
        public List<Transform> PatrolPoints { get; private set; } = new List<Transform>();

        public float Health { get; set; } = 100;

        public bool isDead { get; set; } = false;

        public event Action<float> OnHealthDecreased;

        public void Die()
        {
            if(isDead) return;
            isDead = true;
            enemyAI.enabled = false;
            Invoke(nameof(Hide), 5f);
        }

        private void Hide()
        {
            OnEnemyDead?.Invoke(this);
        }

        public Vector3 ReturnMyPos()
        {
            return this.transform.position;
        }


        public void SetDestination(Vector3 destination)
        {
            enemyAI.SetDestination(destination);
        }

        public void TakeDamage(float damage)
        {
            Health -= damage;
            OnHealthDecreased?.Invoke(Health);
        }

        public void InitializeFromFactory(Transform player, List<Transform> patrolPoints)
        {
            this.PlayerTarget = player;
            this.PatrolPoints = patrolPoints;
        }

        public void Reset()
        {
            Health = 100;
            enemyAI.enabled = true;
            isDead = false;
        }
    }
}
