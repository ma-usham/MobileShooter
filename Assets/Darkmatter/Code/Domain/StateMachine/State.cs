using Darkmatter.Core;
using UnityEngine;

namespace Darkmatter.Domain
{
    public abstract class State<T> : IState
    {
        protected readonly T runner;
        protected State(T runner)
        {
            this.runner = runner;
        }

        public virtual void Enter() { }

        public virtual void Exit() { }

        public virtual void Update() { }

        public virtual void LateUpdate() { }
    }
}
