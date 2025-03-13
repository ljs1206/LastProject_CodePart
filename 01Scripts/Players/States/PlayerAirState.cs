using LJS.Animators;
using LJS.Entities;
using LJS.FSM;
using UnityEngine;

namespace LJS.Players
{
    public abstract class PlayerAirState : EntityState
    {
        protected Player _player;
        protected EntityMover _mover;
        
        public PlayerAirState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _mover = entity.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.SetMovementMultiplier(0.7f);
        }

        public override void Exit()
        {
            _mover.SetMovementMultiplier(1f);
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            // float xInput = _player.PlayerInput.InputDirection.x;
            // if(Mathf.Abs(xInput) > 0)
            //     _mover.SetMovement(xInput);
        }
    }
}
