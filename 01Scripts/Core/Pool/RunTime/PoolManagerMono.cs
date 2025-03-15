using System;
using UnityEngine;

public class PoolManagerMono : MonoBehaviour
{
    [SerializeField] private PoolManagerSO _poolManager;

    private bool _isSpawned;

    private void Awake()
    {
        PoolManagerMono[] objs = FindObjectsByType<PoolManagerMono>(FindObjectsSortMode.None);
        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            _isSpawned = true;
            StartCoroutine(_poolManager.InitializePool(transform));
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (!_isSpawned) return;
        _poolManager.ReleasePoolAsset();
    }
}