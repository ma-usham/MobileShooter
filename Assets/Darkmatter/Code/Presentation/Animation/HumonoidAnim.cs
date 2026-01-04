using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public abstract class HumonoidAnim : MonoBehaviour, IHumonoidAnim
    {
        public Animator animator;

        protected readonly int jumpHash = Animator.StringToHash("Jump");

        public void PlayJumpAnim()
        {
            animator.SetTrigger(jumpHash);
        }
    }
}
