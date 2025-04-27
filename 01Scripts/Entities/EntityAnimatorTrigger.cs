using System;
using LJS.Combat;
using UnityEngine;

namespace LJS.Entities
{
    public class EntityAnimatorTrigger : MonoBehaviour,
        IEntityComponent, IAfterInitable
    {
        public event Action OnAnimationEnd;
        public event Action<string> OnAttackTrigger;
        public event Action<ParticleSystem> OnParticleTrigger;
        
        protected Entity _entity;
        protected Animator _animator;
        protected DamageCaster _casterCompo;
        
        private AnimatorClipInfo[] _animatorClipInfo = new AnimatorClipInfo[1];
        
        public virtual void Initialize(Entity entity)
        {
            _entity = entity;
            _animator = GetComponent<Animator>();
        }
        
        public void AfterInit()
        {
            _casterCompo = _entity.GetCompo<EntityAttack>(true).Caster;
        }
        
        protected virtual void AnimationEnd() => OnAnimationEnd?.Invoke();
        protected virtual void AttackTriggerStart()
        { 
            _casterCompo.CastNow = true;
            _animatorClipInfo = _animator.GetCurrentAnimatorClipInfo(0);
            OnAttackTrigger?.Invoke(_animatorClipInfo[0].clip.name);
        }
        
        protected virtual void AttackTriggerEnd()
        {
            _casterCompo.CastNow = false;
        }
    }
}
