using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Presentation
{
    public class PlayerAimTargetProvider : MonoBehaviour, ITargetProvider
    {
        private Camera mainCamera;
        [SerializeField] private LayerMask aimLayer;

        private RaycastHit _hitPoint;
        public RaycastHit hitPoint => _hitPoint;

        public Vector3 currentAimPos;
        public Transform AimObject; //for IK aim handling
        public float smoothing = 10f;
        public float maxDistance = 100f;

        private void Start()
        {
            mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector2 screenPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = mainCamera.ScreenPointToRay(screenPoint);
            if (Physics.Raycast(ray, out _hitPoint, maxDistance, aimLayer,queryTriggerInteraction:QueryTriggerInteraction.Ignore))
            {
                currentAimPos = Vector3.Lerp(currentAimPos, _hitPoint.point, Time.deltaTime * smoothing);

            }
            else
            {
                currentAimPos = ray.GetPoint(maxDistance);
            }

            AimObject.position = currentAimPos;
        }
    }
}
