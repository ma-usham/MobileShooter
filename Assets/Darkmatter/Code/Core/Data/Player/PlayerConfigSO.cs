using UnityEngine;

namespace Darkmatter.Core
{
    [CreateAssetMenu(fileName = "PlayerConfigSO", menuName = "Scriptable Objects/PlayerConfigSO")]
    public class PlayerConfigSO : ScriptableObject
    {
        public float moveSpeed = 3.0f;
        public float jumpForce = 5f;
        public float gravity = -15f;
    }
}
