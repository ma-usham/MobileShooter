using UnityEngine;

namespace Darkmatter.Core
{
    public interface IState
    {
        void Enter();
        void Update();

        void LateUpdate();
        void Exit();
    }
}
