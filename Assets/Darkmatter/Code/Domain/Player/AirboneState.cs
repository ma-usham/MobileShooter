using Darkmatter.Core;
using System;
using UnityEngine;

namespace Darkmatter.Domain
{
    public class AirboneState : State<PlayerStateMachine>
    {
        public AirboneState(PlayerStateMachine runner) : base(runner) { }
        private IInputReader inputReader => runner.inputReader;
        private PlayerConfigSO playerConfig => runner.playerConfig;


        public override void Enter()
        {
            Debug.Log("Entering Player AirboneState ");
            inputReader.OnReloadPerformed += HandleManualReload;
        }
        public override void Update()
        {
            HandlePlayerMovement();
            HandleShoooting();
            CheckForStateBreak();
        }

        public override void LateUpdate()
        {
            HandlePlayerRotation();
        }

        public override void Exit()
        {
            Debug.Log("Exiting Player AriboneState");
            inputReader.OnReloadPerformed -= HandleManualReload;
        }


        //Airbone Functions
        private void HandlePlayerMovement()
        {
            runner.Move(inputReader.moveInput, playerConfig.moveSpeed);
        }

        private void HandlePlayerRotation()
        {
            runner.RotateCamera(inputReader.lookInput);
        }

        private void CheckForStateBreak()
        {
            if (runner.playerPawn.isGrounded)
            {
                runner.ChangeState(new LocomotionState(runner));
            }
        }

        private void HandleShoooting()
        {
            runner.Shoot(inputReader.isShooting);
        }

        private void HandleManualReload()
        {
            runner.Reload();
        }
    }
}
