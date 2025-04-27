using System;
using LJS.Core.StatSystem;
using LJS.Entities;
using UnityEngine;

namespace LJS.Combat
{
    public class HealthComponent : MonoBehaviour, IDamageable
    {
        [SerializeField] private StatSO _maxHealth;

        public event Action<int> OnHitAction;
        public event Action OnDeathAction;
        
        public float CurrentHealth { get; private set; }
        
        private Entity _entity;
        public void InitHealthCompo(Entity entity)
        {
            _entity = entity;
            CurrentHealth = (int)_maxHealth.Value;
        }
        
        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;
            
            OnHitAction?.Invoke((int)damage);
            if (Mathf.Approximately(CurrentHealth, 0))
            {
                _entity.IsDead = true;
                OnDeathAction?.Invoke();
            }
        }

    }
}
