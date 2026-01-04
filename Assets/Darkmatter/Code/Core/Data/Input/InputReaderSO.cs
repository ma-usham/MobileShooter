using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Darkmatter.Core
{
    [CreateAssetMenu(fileName = "InputReaderSO", menuName = "Scriptable Objects/InputReaderSO")]
    public class InputReaderSO : ScriptableObject, IInputReader, GameInputAction.IPlayerActions
    {
        public GameInputAction action;

        public event Action OnJumpPerformed;
        public event Action OnAdsCameraSwitch;
        public event Action OnReloadPerformed;
        public bool isShooting { get; private set; }
        public Vector2 moveInput { get; private set; }
        public Vector2 lookInput { get; private set; }

 

        private void OnEnable()
        {
            if (action == null)
            {
                action = new GameInputAction();
            }
            action.Enable();
            action.Player.SetCallbacks(this);
        }

        private void OnDisable()
        {
            action.Player.Disable();
        }
        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }


        public void OnLook(InputAction.CallbackContext context)
        {
            lookInput = context.ReadValue<Vector2>();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnJumpPerformed?.Invoke();
            }
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            isShooting = context.ReadValueAsButton();
        }

        public void OnADSCamera(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnAdsCameraSwitch?.Invoke();
            }
        }

        public void OnReload(InputAction.CallbackContext context)
        {
            if(context.performed)
            {
                OnReloadPerformed?.Invoke();
            }
        }

        public void DisableInput()
        {
            action.Player.Disable();
        }

        public void EnableInput()
        {
            action.Player.Enable();
        }
    }
}
