using UnityEngine;

namespace Darkmatter.Core
{
    public interface IEnemyFactory
    {
        IEnemyPawn GetEnemy(ZombieType type);
    }
}
