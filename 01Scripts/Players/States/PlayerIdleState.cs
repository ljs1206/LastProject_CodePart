using LJS.Animators;
using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerIdleState : PlayerGroundState
    {
        public PlayerIdleState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately(false);
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            float yInput = _player.PlayerInput.InputDirection.y;
            
            if (Mathf.Abs(xInput) > 0 || Mathf.Abs(yInput) > 0)
                _player.ChangeState("MOVE");
        }
    }
}
