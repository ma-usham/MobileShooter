using Darkmatter.Core;
using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using VContainer;

namespace Darkmatter.Presentation
{
    public class PlayerAnimController : HumonoidAnim, IPlayerAnim
    {
        private readonly int shootHash = Animator.StringToHash("IsShooting");
        protected readonly int moveXhash = Animator.StringToHash("MoveX");
        protected readonly int moveYhash = Animator.StringToHash("MoveY");
        private readonly int reloadHash = Animator.StringToHash("Reload");
        private readonly int deadHash = Animator.StringToHash("Dead");
        public TwoBoneIKConstraint HandOnGunIK; //for gunHand Ik
        private Coroutine reloadCoroutine;

        public void PlayReloadAnim(IReloadableWeapon reloadableWeapon)
        {
            if (reloadCoroutine == null)
            {
                reloadCoroutine = StartCoroutine(ReloadRoutine(reloadableWeapon));
            }
        }

        IEnumerator ReloadRoutine(IReloadableWeapon reloadableWeapon)
        {
            reloadableWeapon.isReloading = true;
            yield return BlendLayerWeight(1, 1, 0.2f);
            HandOnGunIK.weight = 0f;
            animator.SetTrigger(reloadHash);

            yield return new WaitForSeconds(3f); //gave the length of the animation very bad practice

            yield return BlendLayerWeight(1, 0, 0.2f);
            HandOnGunIK.weight = 1f;
            reloadableWeapon.Reload();
            reloadableWeapon.isReloading = false;
            reloadCoroutine = null;

        }

        IEnumerator BlendLayerWeight(int layerIndex, float targetWeight, float blendTime)
        {
            float startWeight = animator.GetLayerWeight(layerIndex);
            float time = 0f;

            while (time < blendTime)
            {
                time += Time.deltaTime;
                float t = time / blendTime;
                float weight = Mathf.Lerp(startWeight, targetWeight, t);
                animator.SetLayerWeight(layerIndex, weight);
                yield return null;
            }

            animator.SetLayerWeight(layerIndex, targetWeight);
        }


        public void PlayMovementAnim(Vector2 velocity)
        {
            animator.SetFloat(moveXhash, velocity.x, 0.4f, Time.deltaTime);
            animator.SetFloat(moveYhash, velocity.y, 0.4f, Time.deltaTime);
        }

        public void PlayShootAnim()
        {
            Debug.Log("player Shoot");
        }

        public void PlayDeadAnim()
        {
            animator.SetTrigger(deadHash);
        }
    }
}
