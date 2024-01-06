using System.Collections.Generic;

using AncientGlyph.GameScripts.Services.Interfaces;

using UnityEngine;

namespace AncientGlyph.GameScripts.LifeCycle.Services
{
    public class ComponentLocatorService : IComponentLocatorService
    {
        private readonly Dictionary<System.Type, MonoBehaviour> ComponentCache = new Dictionary<System.Type, MonoBehaviour>();

        public TComponent FindComponent<TComponent>() where TComponent : MonoBehaviour
        {
            var type = typeof(TComponent);
            var containInCache = ComponentCache.ContainsKey(type);

            if (containInCache)
            {
                return (TComponent) ComponentCache[type];
            }
            else
            {
                var component = Object.FindFirstObjectByType<TComponent>();

                if (component != null)
                {
                    ComponentCache[type] = component;
                }

                return component;
            }
        }

        public bool IsComponentExist<TComponent>(TComponent thisComponent) where TComponent : MonoBehaviour
        {
            var component = FindComponent<TComponent>();
            return component != null && thisComponent != component;
        }
    }
}