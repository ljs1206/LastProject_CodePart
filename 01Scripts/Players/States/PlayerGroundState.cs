using LJS.Animators;
using LJS.Entities;
using LJS.FSM;
using UnityEngine;

namespace LJS.Players
{
    public abstract class PlayerGroundState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        
        protected PlayerGroundState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _player.PlayerInput.JumpEvent += HandleJump;
        }

        public override void Exit()
        {
            _player.PlayerInput.JumpEvent -= HandleJump;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            // if (_mover.IsGroundDetected() == false && _mover.CanManualMove)
            // {
            //     _player.ChangeState("FALL");
            // }
        }

        private void HandleJump()
        {
            _player.ChangeState("JUMP");
        }
    }
}
