using Darkmatter.Core;
using System;
using Unity.Cinemachine;
using UnityEngine;
using VContainer;

namespace Darkmatter.Presentation
{
    public class CameraService : MonoBehaviour, ICameraService
    {
        public Camera mainCamera { get; private set; }
        public CinemachineThirdPersonFollow AdsCamera;
        [Inject] private IInputReader inputReader;
        public bool isAiming = false;
        private void Start()
        {
            mainCamera = Camera.main;
            inputReader.OnAdsCameraSwitch += SwitchADSCamera;
            AdsCamera.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            inputReader.OnAdsCameraSwitch -= SwitchADSCamera;   
        }

        private void SwitchADSCamera()
        {
            isAiming = !isAiming;
            AdsCamera.gameObject.SetActive(isAiming);
        }
    }
}
