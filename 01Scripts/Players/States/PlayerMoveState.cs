using LJS.FSM;
using LJS.Animators;
using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerMoveState : PlayerGroundState
    {
        public PlayerMoveState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Update()
        {
            base.Update();
            float xInput = _player.PlayerInput.InputDirection.x;
            float yInput = _player.PlayerInput.InputDirection.y;
            _mover.SetMovement(new Vector2(xInput, yInput));

            if (Mathf.Approximately(xInput, 0) &&  Mathf.Approximately(yInput, 0))
            {
                _player.ChangeState("IDLE");
            }
        }
    }
}
