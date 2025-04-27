using System;
using System.Collections.Generic;
using LJS.Entities;
using LJS.Players;
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

        private Player _player;
        
        public void PlayBladeEffect(int comboIndex)
        {
            IPoolable poolable  = _poolManager.Pop(_slashEffectList[comboIndex]);
            poolable.GameObject.transform.position = _slashTrmList[comboIndex].trm.position;

            float effectRotZ = _slashTrmList[comboIndex].rot.z;
            if (Mathf.Approximately(_player.
                    GetCompo<EntityRenderer>().FacingDirection, -1))
            {
                effectRotZ -= 180;
            }
            
            poolable.GameObject.transform.rotation = 
                Quaternion.Euler(new Vector3(_slashTrmList[comboIndex].rot.x,
                    _slashTrmList[comboIndex].rot.y, 
                    effectRotZ));
            poolable.GameObject.GetComponent<ParticleSystem>().Play();
        }
        
        public void Initialize(Entity entity)
        {
            _player = entity as Player;
        }
    }
}
