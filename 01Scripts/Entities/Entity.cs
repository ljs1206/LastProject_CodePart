using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LJS.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        protected Dictionary<Type, IEntityComponent> _components;
        public Transform VisualTrm { get; private set; }
        public bool IsDead { get; set; }

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IEntityComponent>();
            VisualTrm = transform.Find("Visual");
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IEntityComponent>(true)
                .ToList().ForEach(component => _components.Add(component.GetType(), component));
        }
        
        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(component =>  component.Initialize(this));
        }
        
        protected virtual void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitable>()
                .ToList().ForEach(afterInitCompo => afterInitCompo.AfterInit());
        }

        public T GetCompo<T>(bool isDerived = false) where T : IEntityComponent
        {
            if (_components.TryGetValue(typeof(T), out IEntityComponent component))
            {
                return (T)component;
            }

            if (isDerived == false) return default;
            
            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if(findType != null)
                return (T)_components[findType];

            return default;
        }
    }
}
