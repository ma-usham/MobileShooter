using UnityEngine;

namespace Darkmatter.Core
{
    public interface ITargetProvider
    {
        public RaycastHit hitPoint { get; }
    }
}
