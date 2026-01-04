using UnityEngine;

namespace Darkmatter.Core
{
    public interface IReloadableWeapon
    {
        bool canAttack { get; }
        bool isReloading { get; set; }
        int AmmoCount { get; }
        int initialAmmoCount { get; }
        void Reload();
        void Attack();
    }
}
