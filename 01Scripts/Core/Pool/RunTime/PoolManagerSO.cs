using System;
using System.Collections;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;


[CreateAssetMenu(menuName = "SO/Pool/Manager")]
public class PoolManagerSO : ScriptableObject
{
    public event Action CompletedInitEvent;
#if ODIN_INSPECTOR
    [Searchable]
#endif
    public List<PoolingItemSO> poolingItemList = new();

    private Dictionary<string, Pool> _pools;
    private Transform _rootTrm;

    public IEnumerator InitializePool(Transform root)
    {
        _rootTrm = root;
        _pools = new Dictionary<string, Pool>();

        foreach (var item in poolingItemList)
        {
            var handle = item.prefab.LoadAssetAsync<GameObject>();
            if (!handle.IsDone)
                yield return handle;
            IPoolable poolable = handle.Result.GetComponent<IPoolable>();
            Debug.Assert(poolable != null, $"PoolItem does not have IPoolable {handle.Result.name}");

            var pool = new Pool(poolable, _rootTrm, item.initCount);
            _pools.Add(item.poolType.typeName, pool);
        }

        CompletedInitEvent?.Invoke();
    }

    public IPoolable Pop(PoolTypeSO type)
    {
        if (_pools.TryGetValue(type.typeName, out Pool pool))
        {
            return pool.Pop();
        }

        return null;
    }

    public void ReleasePoolAsset()
    {
        foreach (var item in poolingItemList)
        {
            if (item.prefab.IsValid())
                item.prefab.ReleaseAsset();
        }
    }

    public void Push(IPoolable item)
    {
        if (_pools.TryGetValue(item.PoolType.typeName, out Pool pool))
        {
            pool.Push(item);
        }
    }
}