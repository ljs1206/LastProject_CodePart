using LJS.Entities;
using UnityEngine;
using static BehaviorDesigner.Runtime.BehaviorManager;

namespace LJS.Enemy
{
    public class Enemy : Entity
    {
        [field:SerializeField] public BehaviorTree BTCompo { get; private set; }
    }
}
