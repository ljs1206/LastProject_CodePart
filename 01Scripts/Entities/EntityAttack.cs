using LJS.Animators;
using LJS.Combat;
using UnityEngine;

namespace LJS.Entities
{
    public abstract class EntityAttack : MonoBehaviour, IEntityComponent, IAfterInitable
    {
        [Header("PoolTypeSO")]
        [SerializeField]
        private PoolTypeSO _slash1;
        
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
        private Entity _entity;
        private EntityAnimatorTrigger _entityAnimatorTrigger;
        private DamageCaster _caster;
        public DamageCaster Caster => _caster;

        public void Initialize(Entity entity)
        {
            _entity = entity as Entity;

            _entityAnimatorTrigger = _entity.GetCompo<EntityAnimatorTrigger>(true);
            _caster = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        }

        public virtual void AfterInit()
        {
            _entityAnimatorTrigger.OnAttackTrigger += HandleAttackTrigger;
        }

        protected abstract void HandleAttackTrigger(string animationName);
    }
}
