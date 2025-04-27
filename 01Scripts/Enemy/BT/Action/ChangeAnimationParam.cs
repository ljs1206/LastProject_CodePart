using BehaviorDesigner.Runtime.Tasks;
using LJS.Animators;
using LJS.Entities;
using UnityEngine;

namespace LJS.Enemy
{
    public struct ParamType
    {
        public float floatValue;
        public int intValue;
        public bool boolValue;
    }

    public class SetAnimationParam : Action
    {
        [SerializeField] private AnimParamSO _param;
        [SerializeField] private ParamType _paramType;

        private Entity _entity;
        private AnimateRenderer _animator;

        public override void OnAwake()
        {
            _entity = GetComponent<Entity>();
            _animator = GetComponent<AnimateRenderer>();
        }

        public override void OnStart()
        {
            _animator.SetParam(_param);
        }
    }
}

