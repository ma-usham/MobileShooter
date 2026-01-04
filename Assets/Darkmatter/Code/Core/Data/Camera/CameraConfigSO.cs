using UnityEngine;

namespace Darkmatter.Core
{
    [CreateAssetMenu(fileName = "CameraConfigSO", menuName = "Scriptable Objects/CameraConfigSO")]
    public class CameraConfigSO : ScriptableObject
    {
        public float lookSensitivity = 10f;
        public float topClampAngle = 50f;
        public float bottomClampAngle = -30f;
    }
}
