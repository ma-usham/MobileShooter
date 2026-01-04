using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public class EnemyAnimController : HumonoidAnim, IEnemyAnimController
    {
        private readonly int walkHash = Animator.StringToHash("walk");
        private readonly int chaseHash = Animator.StringToHash("chase");
        private readonly int attackHash = Animator.StringToHash("attack");
        private readonly int deadHash = Animator.StringToHash("dead");

        public void PlayWalkAnim(bool value)
        {
            animator.SetBool(walkHash, value);
        }

        public void PlayAttackAnim(bool value)
        {
            animator.SetBool(attackHash, value);
        }

        public void PlayeChaseAnim(bool value)
        {
            animator.SetBool(chaseHash, value);
        }

        public void PlayDeadAnim()
        {
            resetValues();
            animator.SetTrigger(deadHash);
        }

        public void resetValues()
        {
            animator.SetBool("walk", false);
            animator.SetBool("chase", false);
            animator.SetBool("attack", false);
        }
    }
}
