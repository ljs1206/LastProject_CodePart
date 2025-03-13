using LJS.Animators;
using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerJumpState : PlayerAirState
    {
        public PlayerJumpState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
        }

        public override void Enter()
        {
            base.Enter();
            // Vector2 jumpPower = new Vector2(0, _player.JumpPowerStat.Value); //todo : Stat기반으로 진행
            //
            // _player.DecreaseJumpCount(); //점프카운트 감소
            // _mover.StopImmediately(true);
            // _mover.AddForceToEntity(jumpPower);
            _mover.OnMovement += HandleVelocityChange;
        }

        public override void Exit()
        {
            _mover.OnMovement -= HandleVelocityChange;
            base.Exit();
        }

        private void HandleVelocityChange(Vector2 velocity)
        {
            if(velocity.y < 0)
                _player.ChangeState("FALL"); //떨어지는 상태로 변경
        }
    }
}
