using UnityEngine;

namespace Darkmatter.Core
{
    public interface IEnemyAnimController
    {
        public void PlayWalkAnim(bool value);
        public void PlayAttackAnim(bool value);
        public void PlayeChaseAnim(bool value);
        public void PlayDeadAnim();
    }
}
