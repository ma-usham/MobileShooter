using UnityEngine;
using System;

namespace Darkmatter.Core
{
    public interface IDamageable
    {

        event Action<float> OnHealthDecreased;
        float Health { get; set; }
        void TakeDamage(float damage);
        void Die();
    }
}
