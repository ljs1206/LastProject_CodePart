using System;
using UnityEngine;
using UnityEngine.Pool;

namespace LJS.Entities
{
    public class EffectBase : MonoBehaviour, IPoolable
    {
        private ParticleSystem _ps;
        private bool _check;
        
        [SerializeField] private PoolManagerSO _poolManager;
        
        #region Pool

        [SerializeField] private PoolTypeSO _poolType;
        public PoolTypeSO PoolType => _poolType;
        public GameObject GameObject => gameObject;
        public void SetUpPool(Pool pool)
        {
            _ps = GetComponent<ParticleSystem>();
        }

        public void ResetItem()
        {
            _check = true;
        }

        #endregion

        private void Update()
        {
            if(_check && _ps.isStopped)
                _poolManager.Push(this);
        }
    }
}
