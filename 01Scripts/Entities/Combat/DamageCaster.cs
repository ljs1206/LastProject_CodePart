using System;
using System.Collections;
using LJS.Core.StatSystem;
using LJS.Define;
using LJS.Entities;
using UnityEngine;

namespace LJS.Combat
{
    [Serializable]
    public struct CombatData
    {
        public Vector3 dir;
        public int damage;
        public Vector2 point;
        public AttackType attackType;
        public Vector2 offset;
        public Vector2 castSize;
    }
    
    public class DamageCaster : MonoBehaviour
    {
        [Header("Damage Caster")]
        [SerializeField] private Vector2 _offSet;
        [SerializeField] private Vector2 _size;
        [SerializeField] private LayerMask _whatIsTarget;

        [Header("Damage Data")] 
        [SerializeField] private StatSO _power;
        
        private CombatData _combatData;
        private Vector3 _castPos;
        private Entity _entity;

        public bool CastNow { get; set; }

        public void InitCaster(Entity entity)
        {
            _entity = entity;
            _combatData = new CombatData();
        }

        public void SetCombatData(CombatData data, bool start = false)
        {
            _combatData = data;
            if (start)
                StartCoroutine(DelayCasting());
        }

        private IEnumerator DelayCasting()
        {
            while (CastNow)
            {
                CastDamage(_combatData);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public void CastDamage(CombatData combatData)
        {
            _combatData = combatData;
            _castPos = (Vector2)transform.position + combatData.offset;

            Collider2D[] result
                = Physics2D.OverlapBoxAll(_castPos,
                    combatData.castSize, 0, _whatIsTarget);

            if (result.Length > 0)
            {
                foreach (var col in result)
                {
                    if (col.TryGetComponent(out IDamageable target))
                    {
                        target.ApplyDamage(_power.Value);
                        // todo : Other Settings
                    }
                }
            }
        }

#if UNITY_EDITOR

        private void OnDrawGizmos()
        {
            if (!CastNow) return;
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube((Vector2)transform.position + _combatData.offset,
                _combatData.castSize);
        }
        
        #endif
    }
}
