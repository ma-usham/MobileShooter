using Darkmatter.Core;
using Darkmatter.Domain;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.SceneManagement;
using VContainer;

namespace Darkmatter.Presentation
{
    public class PlayerMotor : MonoBehaviour, IPlayerPawn
    {

        [Header("LookSetting")]
        public Transform cinemachineFollowTarget;

        [Header("MoveSetting")]
        public CharacterController characterController;

        [Header("GravitySetting")]
        private float verticalVelocity;
        public bool isGrounded => IsOnGround();

        public float Health { get; set; } = 100;

        [Header("GroundCheckSensorSetting")]
        public float groundOffset;
        public float groundCheckRadius;
        public LayerMask groundLayer;

        [Header("TurnSetting")]
        public float turnSpeed = 5f;
        [Inject] private PlayerConfigSO playerConfig;
        [Inject] private IPlayerAnim PlayerAnim;
        [Inject] private IGameScreenController gameScreenController;
        [Inject] private IInputReader inputReader;

        public event Action<float> OnHealthDecreased;

        private bool isDead=false;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        } 

        //state based functions
        public void Move(Vector3 motion)
        {
            ApplyGravity(); //apply gravity before moving
            Vector3 finalMove = motion;
            finalMove.y = verticalVelocity;
            characterController.Move(finalMove * Time.deltaTime);
        }

        public void SetCameraRotation(float pitch, float yaw)
        {
            cinemachineFollowTarget.rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,yaw,0), Time.deltaTime*turnSpeed); //rotate player towards the camera forward axis
        }

        public void Jump(float jumpForce)
        {
            verticalVelocity = jumpForce;
        }

        public bool IsOnGround()
        {
            Vector3 groundPos = transform.position + Vector3.down * groundOffset;

            return Physics.CheckSphere(
                groundPos,
                groundCheckRadius,
                groundLayer,
                QueryTriggerInteraction.Ignore
            );

        }
        public void ApplyGravity()
        {
            if (isGrounded && verticalVelocity < 0f)
            {
                verticalVelocity = -2f; // snap to ground
            }
            verticalVelocity += playerConfig.gravity * Time.deltaTime;
        }
        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Vector3 groundPos = transform.position + Vector3.down * groundOffset;
            Gizmos.DrawWireSphere(groundPos, groundCheckRadius);
        }
        float damageCooldown = 1f;
        float lastHitTime;

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Enemy")&& !isDead)
            {
                TakeDamage(10);
            }
        }

        public void TakeDamage(float damage)
        {
            Health-=damage;
            if(Health<=0)
            {
                Die();
            }
            gameScreenController.ShowPlayerHealth((int)Health);
        }

        public void Die()
        {
            isDead = true;
            PlayerAnim.PlayDeadAnim();
            inputReader.DisableInput();
            Invoke("GameOver", 4f);
        }

        private void GameOver()
        {
            gameScreenController.ShowGameOverText();
            
        }
    }


}
