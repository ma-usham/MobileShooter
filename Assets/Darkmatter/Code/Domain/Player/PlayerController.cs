using VContainer;
using VContainer.Unity;

namespace Darkmatter.Domain
{
    public class PlayerController : IStartable, ITickable, ILateTickable
    {
        [Inject] private PlayerStateMachine psm;

        public void LateTick()
        {
            psm.LateUpdate();
        }

        public void Start()
        {
            psm.ChangeState(new LocomotionState(psm));
        }

        public void Tick()
        {
            psm.Update();
        }
    }
}
