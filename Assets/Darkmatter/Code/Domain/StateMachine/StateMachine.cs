using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Domain
{
    public abstract class StateMachine
    {
        public IState CurrentState { get; private set; }
        public void ChangeState(IState state)
        {
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState?.Enter();
        }

        public void Update()
        {
            CurrentState.Update();
        }

        public void LateUpdate()
        {
            CurrentState.LateUpdate();
        }

    }
}
