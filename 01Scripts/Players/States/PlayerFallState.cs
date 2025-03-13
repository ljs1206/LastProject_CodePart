using LJS.Animators;
using LJS.Entities;

namespace LJS.Players
{
    public class PlayerFallState : PlayerAirState
    {
        public PlayerFallState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            // if (_mover.IsGroundDetected())
            // {
            //     _player.ChangeState("IDLE");
            // }
        }
    }
}