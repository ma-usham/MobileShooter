using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Darkmatter.Core
{
    public interface IEnemyPawn : IDamageable
    {
        
        GameObject GameObject { get; } 
        void InitializeFromFactory(Transform player,List<Transform> patrolPoints);
        bool isDead { get; set; }
        NavMeshAgent EnemyAI { get; }
        List<Transform> PatrolPoints { get; }
        void SetDestination(Vector3 destination);
        Vector3 ReturnMyPos();
        Transform PlayerTarget { get; }
        void Reset();
    }
}
