using System;
using System.Collections.Generic;
using LJS.Entities;
using UnityEngine;

[Serializable]
public struct SlashInfo
{
    public Transform trm;
    public Vector3 rot;
}

namespace LJS
{
    public class PlayerEffect : MonoBehaviour, IEntityComponent
    {
        [SerializeField] private PoolManagerSO _poolManager = null;
        public List<PoolTypeSO> _slashEffectList = new List<PoolTypeSO>();
        [field:SerializeField] private List<SlashInfo> _slashTrmList = new List<SlashInfo>();
        public void PlayBladeEffect(int comboIndex)
        {
            IPoolable poolable  = _poolManager.Pop(_slashEffectList[comboIndex]);
            poolable.GameObject.transform.position = _slashTrmList[comboIndex].trm.position;
            poolable.GameObject.transform.rotation = 
                Quaternion.Euler(_slashTrmList[comboIndex].rot);
            poolable.GameObject.GetComponent<ParticleSystem>().Play();
        }
        
        public void Initialize(Entity entity)
        {
            
        }
    }
}
