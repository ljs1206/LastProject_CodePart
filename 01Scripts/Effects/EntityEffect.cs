using System.Collections.Generic;
using LJS.Entities;
using LJS.Players;
using UnityEngine;

public class EntityEffect : MonoBehaviour, IEntityComponent
{
    [SerializeField] private List<PoolTypeSO> _effctPoolTypeList;

    private Player _player;
    public void Initialize(Entity entity)
    {
        _player = entity as Player;
    }

    public void PlayEffect(PoolTypeSO poolType)
    {
        PoolTypeSO effectPoolType = _effctPoolTypeList.Find(x => poolType == x);
        
        IPoolable effect = _player.PoolManager.Pop(effectPoolType);
    }
}
