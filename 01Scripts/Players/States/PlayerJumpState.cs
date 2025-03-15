using DG.Tweening;
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
            Vector2 jumpPower = new Vector2(0, _player.JumpPowerStat.Value); //todo : Stat기반으로 진행
            
            _mover.StopImmediately(true);
            _mover.Jump(Vector2.up * jumpPower, Ease.OutQuad, Ease.InQuad
                , () => _player.ChangeState("FALL") ,() => _player.ChangeState("IDLE"));
        }

        public override void Update()
        {
            base.Update();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
