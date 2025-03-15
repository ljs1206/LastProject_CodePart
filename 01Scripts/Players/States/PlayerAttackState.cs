using LJS.Animators;
using LJS.Entities;
using LJS.FSM;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerAttackState : EntityState
    {
        private Player _player;
        private PlayerAttack _attackCompo;
        private EntityMover _mover;
        private bool _canCombo;
        
        public PlayerAttackState(Entity entity, AnimParamSO stateAnimParam) : base(entity, stateAnimParam)
        {
            _player = entity as Player;
            _attackCompo = _player.GetCompo<PlayerAttack>();
            _mover = _player.GetCompo<EntityMover>();
        }

        public override void Enter()
        {
            base.Enter();
            _mover.StopImmediately();
            
            _canCombo = false;

            _player.PlayerInput.AttackEvent += HandleAttack;
        }

        private void HandleAttack()
        {
            _canCombo = true;
        }

        public override void Exit()
        {
            base.Exit();
            _player.PlayerInput.AttackEvent -= HandleAttack;
        }

        public override void AnimationEndTrigger()
        {
            base.AnimationEndTrigger();
            Debug.Log(_attackCompo.CurrentComboCount);
            if (_attackCompo.CurrentComboCount + 1 >= _attackCompo.MaxComboCount)
            {
                _attackCompo.CurrentComboCount = 0;
                
                _renderer.SetParam(_stateAnimParam, false);
                _animTrigger.OnAnimationEnd -= AnimationEndTrigger;
                
                _renderer.SetParam(_attackCompo.ComboCountParam, 0);
                _player.ChangeState("IDLE");
                return;
            }
            
            if (_canCombo)
            {
                _renderer.SetParam(_attackCompo.ComboCountParam, ++_attackCompo.CurrentComboCount);
                _player.ChangeState("ATTACK");
            }
            else
            {
                _attackCompo.CurrentComboCount = 0;
                
                _renderer.SetParam(_stateAnimParam, false);
                _renderer.SetParam(_attackCompo.ComboCountParam, 0);
                _animTrigger.OnAnimationEnd -= AnimationEndTrigger;
                
                _player.ChangeState("IDLE");
            }
        }
    }
}
