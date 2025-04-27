using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStarManager : MonoSingleton<AStarManager>
{
    // // 생성 된 적의 정보를 알고 있어야 함
    // public List<Enemy> CurrentEnemyList { get; private set; } = new();
    //
    // private AStar _aStarCompo;
    // private void Start()
    // {
    //     CurrentEnemyList = FindObjectsByType<Enemy>(FindObjectsSortMode.None).ToList();
    //     _aStarCompo = new AStar();
    // }
    //
    // public Vector3[] GetCurrentPath(Enemy enemy, Vector3 target)
    // {
    //     return _aStarCompo.FindPath(target, enemy.transform.position);
    // }
}
