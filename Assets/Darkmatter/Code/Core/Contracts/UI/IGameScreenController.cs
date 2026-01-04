using UnityEngine;

namespace Darkmatter.Core
{
    public interface IGameScreenController
    {

        void UpdateFireableBulletCount(int bulletCount);
        void UpdateRemainingZombiesCount(int zombiesCount);
        void UpdateTotalZombiesCount(int totalZombiesCount);
        void ShowGameOverText();
        void ShowPlayerHealth(int health);
    }
}
