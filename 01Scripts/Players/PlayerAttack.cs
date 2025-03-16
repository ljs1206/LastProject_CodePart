using System.Collections.Generic;
using LJS.Animators;
using LJS.Entities;
using UnityEngine;

namespace LJS.Players
{
    public class PlayerAttack : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [Header("PoolTypeSO")]
        [SerializeField]
        private PoolTypeSO _slash1;
        private PoolTypeSO _slash2;
        
        // [Header("CombatData")]
        // private 
        
        public List<int> AttackMoveList = new();
        
        public int MaxComboCount;

        private int _currentComboCount = 0;
        public int CurrentComboCount
        {
            get
            {
                return _currentComboCount;
            }
            set
            {
                if (value < MaxComboCount)
                {
                    _currentComboCount = value;
                }
            }
        }
        
        public AnimParamSO ComboCountParam;
        private Player _player;
        private EntityAnimatorTrigger _entityAnimatorTrigger;
        private DamageCaster _caster;

        public void Initialize(Entity entity)
        {
            _player = entity as Player;

            _entityAnimatorTrigger = _player.GetCompo<EntityAnimatorTrigger>(true);
            _caster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        }

        public void AfterInit()
        {
            _entityAnimatorTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        private void HandleAttackTrigger()
        {
            // _caster
        }
    }
}
