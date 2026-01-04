using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class LocomotionState : State<PlayerStateMachine>
    {
        public LocomotionState(PlayerStateMachine runner) : base(runner) { }
        private IInputReader inputReader => runner.inputReader;
        private PlayerConfigSO playerConfig => runner.playerConfig;
        private IPlayerAnim playerAnim => runner.playerAnim;
        public override void Enter()
        {
            Debug.Log("Starting player Locomotion");
            inputReader.OnJumpPerformed += HandlePlayerJump;
            inputReader.OnReloadPerformed += HandleManualReload;
        }

        public override void Update()
        {
            HandlePlayerMovement();
            HandleShooting();
            CheckForStateBreak();
           
        }


        public override void LateUpdate()
        {
            HandlePlayerRotation();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Player Locomotion State");
            inputReader.OnJumpPerformed -= HandlePlayerJump;
            inputReader.OnReloadPerformed -= HandleManualReload;
        }


        //Locomotion Functions

        private void CheckForStateBreak()
        {
            if (!runner.playerPawn.isGrounded)
            {
                runner.ChangeState(new AirboneState(runner));
            }
        }

        private void HandlePlayerRotation()
        {
            runner.RotateCamera(inputReader.lookInput);
        }

        private void HandlePlayerMovement()
        {
            runner.Move(inputReader.moveInput, playerConfig.moveSpeed);
        }

        private void HandlePlayerJump()
        {
            runner.playerPawn.Jump(playerConfig.jumpForce);
            playerAnim.PlayJumpAnim();
        }

        private void HandleShooting()
        {
            runner.Shoot(inputReader.isShooting);
        }

        private void HandleManualReload()
        {
            runner.Reload();
        }





    }
}
