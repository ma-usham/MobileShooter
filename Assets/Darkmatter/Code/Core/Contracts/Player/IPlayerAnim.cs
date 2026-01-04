using System.Threading.Tasks;
using UnityEngine;

namespace Darkmatter.Core
{
    public interface IPlayerAnim : IHumonoidAnim
    {
        public void PlayMovementAnim(Vector2 velocity);
        public void PlayReloadAnim(IReloadableWeapon reloadableWeapon);
        void PlayShootAnim();
        void PlayDeadAnim();
    }
}
