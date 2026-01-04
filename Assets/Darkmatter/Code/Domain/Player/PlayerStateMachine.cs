using Darkmatter.Core;
using System;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace Darkmatter.Domain
{
    public class PlayerStateMachine : StateMachine
    {
       [Inject] public readonly IPlayerPawn playerPawn;
       [Inject] public readonly IInputReader inputReader;
       [Inject] public readonly IPlayerAnim playerAnim;
       [Inject] public readonly ITargetProvider targetProvider;
       [Inject] public readonly ICameraService cameraService; 
       [Inject] public readonly IReloadableWeapon currentWeapon;
       [Inject] public readonly PlayerConfigSO playerConfig;
       [Inject] public readonly CameraConfigSO cameraConfig;
       [Inject] public readonly IAudioService audioService;
        [Inject] public readonly IGameScreenController gameScreenController;

        private Vector3 moveDir;
        private float Yaw;
        private float pitch;

        public void Move(Vector2 moveInputDir, float moveSpeed)
        {
            //player movement with reference to camera
            Vector3 cameraForward =cameraService.mainCamera.transform.forward;
            Vector3 cameraRight = cameraService.mainCamera.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            moveDir = cameraRight * moveInputDir.x + cameraForward * moveInputDir.y;

            playerPawn.Move(moveDir * moveSpeed);
            playerAnim.PlayMovementAnim(moveInputDir);
        }

        public void RotateCamera(Vector2 lookInput)
        {
            //camera rotation logic
            if (lookInput.sqrMagnitude > 0.01f)
            {
                Yaw += lookInput.x * cameraConfig.lookSensitivity * Time.deltaTime;
                pitch -= lookInput.y * cameraConfig.lookSensitivity * Time.deltaTime;
            }
            pitch = Mathf.Clamp(pitch, cameraConfig.bottomClampAngle, cameraConfig.topClampAngle);
            playerPawn.SetCameraRotation(pitch, Yaw);
        }

        public void Shoot(bool isShooting)
        {
            if (!isShooting) return;
            if(currentWeapon.canAttack)
            {
                audioService.PlaySFX(AudioId.Gun_Fire,0.1f);
                currentWeapon.Attack();
                gameScreenController.UpdateFireableBulletCount(currentWeapon.AmmoCount);
            }
            if (currentWeapon.AmmoCount == 0 && !currentWeapon.isReloading)
            {
                audioService.PlaySFX(AudioId.Gun_Reload, 0.1f);
                playerAnim.PlayReloadAnim(currentWeapon);
                gameScreenController.UpdateFireableBulletCount(40);

            }
        }

        public void Reload()
        {
          if(currentWeapon.AmmoCount<currentWeapon.initialAmmoCount && !currentWeapon.isReloading)
            {
                audioService.PlaySFX(AudioId.Gun_Reload, 0.1f);
                playerAnim.PlayReloadAnim(currentWeapon);
                gameScreenController.UpdateFireableBulletCount(40);
            }
            
        }
    }
}
